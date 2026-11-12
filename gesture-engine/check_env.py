"""Vérification de l'environnement NETVERSE VR (Gesture Core).

Usage :
    .\\.venv\\Scripts\\python.exe check_env.py

Le script contrôle :
  1. la présence et la version de MediaPipe, OpenCV, NumPy ;
  2. l'API MediaPipe réellement disponible (Tasks, l'API `mp.solutions` ayant
     disparu en 1.x) ;
  3. l'ouverture de la webcam et la lecture d'images ;
  4. le chargement du modèle `models/hand_landmarker.task` ;
  5. la détection d'une main et la disponibilité des 21 landmarks.

Il ne produit aucune sortie graphique : il est exécutable sans écran.
"""

from __future__ import annotations

import os
import pathlib
import sys
import time

# Matplotlib est importé transitivement par MediaPipe et tente d'écrire dans
# %LOCALAPPDATA%. On le redirige vers un dossier local au projet.
_ROOT = pathlib.Path(__file__).resolve().parent
os.environ.setdefault("MPLCONFIGDIR", str(_ROOT / ".cache" / "matplotlib"))

MODEL_PATH = _ROOT / "models" / "hand_landmarker.task"


def titre(texte: str) -> None:
    print()
    print("=" * 62)
    print(texte)
    print("=" * 62)


def versions() -> bool:
    titre("1. Versions")
    ok = True
    try:
        import numpy as np

        print(f"  numpy        : {np.__version__}")
    except Exception as exc:  # pragma: no cover - diagnostic
        print(f"  numpy        : ABSENT ({exc})")
        ok = False

    try:
        import cv2

        print(f"  opencv       : {cv2.__version__}")
    except Exception as exc:  # pragma: no cover
        print(f"  opencv       : ABSENT ({exc})")
        ok = False

    try:
        import mediapipe as mp

        print(f"  mediapipe    : {mp.__version__}")
        print(f"  python       : {sys.version.split()[0]}")
        print(f"  mp.solutions : {hasattr(mp, 'solutions')}  (API historique)")
        print(f"  mp.tasks     : {hasattr(mp, 'tasks')}  (API actuelle)")
    except Exception as exc:  # pragma: no cover
        print(f"  mediapipe    : ABSENT ({exc})")
        ok = False

    if MODEL_PATH.exists():
        print(f"  modèle       : {MODEL_PATH.name} — présent "
              f"({MODEL_PATH.stat().st_size / 1e6:.2f} Mo)")
    else:
        print(f"  modèle       : {MODEL_PATH.name} — ABSENT")

    return ok and MODEL_PATH.exists()


def camera(num_frames: int = 20) -> tuple[bool, object | None]:
    titre("2. Webcam")
    import cv2

    for nom, backend in (("DirectShow", cv2.CAP_DSHOW),
                         ("MSMF", cv2.CAP_MSMF),
                         ("défaut", cv2.CAP_ANY)):
        cap = cv2.VideoCapture(0, backend)
        if not cap.isOpened():
            cap.release()
            print(f"  [{nom}] ouverture impossible")
            continue

        largeur = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
        hauteur = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
        print(f"  [{nom}] ouverte — {largeur}x{hauteur}")

        debut = time.perf_counter()
        lues = 0
        frame = None
        for _ in range(num_frames):
            ok, img = cap.read()
            if ok and img is not None:
                lues += 1
                frame = img
        duree = time.perf_counter() - debut
        fps = lues / duree if duree > 0 else 0.0
        print(f"  [{nom}] {lues}/{num_frames} images lues — ~{fps:.1f} i/s")
        cap.release()

        if frame is not None:
            return True, frame

    return False, None


def detection(frame) -> bool:
    titre("3. Détection de main (MediaPipe Tasks)")
    import cv2
    import mediapipe as mp
    from mediapipe.tasks import python as mp_python
    from mediapipe.tasks.python import vision

    options = vision.HandLandmarkerOptions(
        base_options=mp_python.BaseOptions(model_asset_path=str(MODEL_PATH)),
        running_mode=vision.RunningMode.IMAGE,
        num_hands=1,
        min_hand_detection_confidence=0.5,
        min_hand_presence_confidence=0.5,
        min_tracking_confidence=0.5,
    )

    with vision.HandLandmarker.create_from_options(options) as landmarker:
        rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        mp_image = mp.Image(image_format=mp.ImageFormat.SRGB, data=rgb)
        debut = time.perf_counter()
        resultat = landmarker.detect(mp_image)
        ms = (time.perf_counter() - debut) * 1000.0

    mains = resultat.hand_landmarks or []
    print(f"  inférence    : {ms:.0f} ms")
    print(f"  mains        : {len(mains)}")

    if not mains:
        print("  → aucune main dans l'image (modèle chargé et exécuté correctement)")
        print("    Placez votre main devant la caméra et relancez pour valider les 21 points.")
        return True

    points = mains[0]
    print(f"  landmarks    : {len(points)}")
    pouce, index, poignet, majeur = points[4], points[8], points[0], points[9]
    d = ((pouce.x - index.x) ** 2 + (pouce.y - index.y) ** 2) ** 0.5
    paume = ((poignet.x - majeur.x) ** 2 + (poignet.y - majeur.y) ** 2) ** 0.5
    print(f"  pouce-index  : {d:.4f} (normalisé image)")
    print(f"  taille paume : {paume:.4f}")
    print(f"  ratio        : {(d / paume if paume else 0):.4f}  ← grandeur utile au Gesture Core")
    return True


def main() -> int:
    if not versions():
        print("\nÉCHEC : dépendances ou modèle manquant.")
        return 1

    # Mode image fixe : utile pour valider le modèle sans webcam.
    #   python check_env.py chemin\vers\photo.jpg
    if len(sys.argv) > 1:
        import cv2

        chemin = pathlib.Path(sys.argv[1])
        titre(f"2. Image fixe : {chemin.name}")
        frame = cv2.imread(str(chemin))
        if frame is None:
            print("  ÉCHEC : image illisible.")
            return 2
        print(f"  image        : {frame.shape[1]}x{frame.shape[0]}")
        detection(frame)
        print("\nOK — chaîne MediaPipe opérationnelle (image fixe).")
        return 0

    ok_cam, frame = camera()
    if not ok_cam:
        print("\nÉCHEC : aucune image obtenue de la webcam.")
        return 2

    detection(frame)
    print("\nOK — environnement opérationnel.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())

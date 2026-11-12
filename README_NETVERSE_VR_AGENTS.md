# NETVERSE VR — README de référence pour les agents IA

> **Document de passation technique et fonctionnelle**  
> Ce fichier est destiné aux agents IA qui interviendront directement dans le dépôt local du projet. Il rassemble la vision, les décisions prises, les contraintes, l'architecture cible, les comportements attendus et les priorités de développement convenus avec le porteur du projet.
>
> **Règle fondamentale :** ce README est la référence fonctionnelle. Avant toute modification, l'agent doit inspecter le projet réel, comprendre ce qui existe déjà et préserver le travail effectué. Ne jamais supposer qu'un module est absent simplement parce qu'il n'est pas décrit ici.

---

## 1. Présentation du projet

### 1.1 Nom

**NETVERSE VR**

### 1.2 Concept

NETVERSE VR est un environnement 3D immersif de formation et de simulation des réseaux informatiques. L'utilisateur y joue le rôle d'un administrateur réseau chargé de concevoir, installer, configurer, tester et dépanner le réseau informatique d'un établissement virtuel : université, entreprise, école technique, centre de formation ou organisation.

L'utilisateur doit pouvoir se déplacer dans les locaux, observer les salles et les équipements, installer du matériel, relier les appareils par des câbles Ethernet, configurer les paramètres réseau et vérifier le fonctionnement de l'infrastructure.

Le projet ne doit pas être un simple décor 3D. Il doit devenir un **laboratoire réseau interactif**, où chaque action a une conséquence et où l'utilisateur apprend en pratiquant.

### 1.3 Problème à résoudre

L'apprentissage des réseaux informatiques repose souvent sur des équipements physiques qui peuvent être coûteux, indisponibles, insuffisants en nombre ou difficiles à manipuler pour les débutants.

NETVERSE VR vise à permettre aux apprenants de :

- pratiquer sans devoir disposer immédiatement d'un laboratoire physique complet ;
- comprendre visuellement la topologie d'un réseau ;
- apprendre à installer et relier des équipements ;
- expérimenter des configurations IP ;
- observer les conséquences d'une mauvaise configuration ;
- diagnostiquer des pannes dans un environnement sans risque matériel ;
- répéter des exercices autant de fois que nécessaire.

### 1.4 Public cible

- Universités et instituts supérieurs ;
- écoles techniques et centres de formation ;
- enseignants et formateurs en réseaux ;
- étudiants en informatique ;
- entreprises qui souhaitent former ou sensibiliser leurs équipes techniques.

### 1.5 Proposition de valeur

NETVERSE VR permet de **s'entraîner avant d'intervenir sur une infrastructure réelle**. Il réduit la dépendance à un laboratoire physique pour les premiers apprentissages, rend les concepts réseau plus visuels et permet de simuler des erreurs et des scénarios de dépannage de manière reproductible.

---

## 2. Vision de l'expérience utilisateur

L'expérience doit donner l'impression d'entrer dans un établissement réel transformé en laboratoire réseau.

L'utilisateur incarne un administrateur réseau. Il peut notamment :

1. se déplacer dans les locaux ;
2. regarder autour de lui dans toutes les directions ;
3. cibler un objet ou un équipement ;
4. faire apparaître un menu contextuel dans l'environnement ;
5. sélectionner une action avec un geste ;
6. installer ou déplacer un équipement ;
7. connecter des équipements avec un câble ;
8. configurer leurs paramètres ;
9. lancer des tests de connectivité ;
10. visualiser les résultats et comprendre les erreurs.

L'interface doit être visuelle, claire, pédagogique et suffisamment intuitive pour être utilisée sans formation préalable longue.

### 2.1 Nature de l'expérience

La cible actuelle est une **simulation 3D immersive pilotée par gestes et webcam**, sans casque VR obligatoire.

Ne pas présenter techniquement la version sans casque comme une expérience VR stéréoscopique avec suivi spatial complet. La formulation juste est : environnement 3D immersif contrôlé par gestes, avec une ambition d'évolution vers des dispositifs VR si le matériel devient disponible.

### 2.2 Matériel d'entrée

Pour la démonstration prévue :

- une webcam, idéalement externe ;
- un ordinateur Windows ;
- aucune manette obligatoire ;
- aucun smartphone obligatoire ;
- aucun casque VR obligatoire ;
- l'utilisateur ne doit pas dépendre du clavier ou de la souris pendant l'expérience gestuelle normale.

Le clavier et la souris peuvent rester disponibles pour le développement, le débogage, les réglages ou les opérations administratives, mais ils ne doivent pas constituer le mode normal d'interaction de l'utilisateur final.

---

## 3. Décisions techniques déjà convenues

### 3.1 Stack principale

| Domaine | Technologie retenue | Responsabilité |
|---|---|---|
| Monde 3D et application | Godot 4 .NET | Scènes, rendu 3D, physique, interface et interactions |
| Langage applicatif | C# | Logique de simulation, objets, équipements et réseau simulé |
| Vision par ordinateur | Python | Capture webcam et traitement des images |
| Détection de la main | MediaPipe | Extraction des 21 points de repère de la main |
| Capture vidéo | OpenCV | Accès caméra et prétraitement des images |
| Communication locale | UDP sur localhost | Transmission des commandes gestuelles vers Godot |
| Apprentissage personnalisé | Scikit-learn, si nécessaire | Classification de gestes personnalisés pour les premières versions |

### 3.2 Principes d'architecture

- Godot est responsable du monde 3D et de l'expérience interactive.
- Python est responsable de l'analyse de la main et de la reconnaissance gestuelle.
- Les deux processus communiquent localement, de préférence par UDP, avec des messages structurés.
- Le moteur réseau pédagogique est d'abord simulé en C# dans l'application.
- Ne pas introduire Docker, des machines virtuelles ou une véritable infrastructure réseau lourde dans le MVP, sauf demande explicite ultérieure.
- Les ressources 3D doivent rester légères : modèles low-poly, textures raisonnables et scènes optimisées.
- La solution doit fonctionner sur un ordinateur aux ressources modestes.

### 3.3 Environnement de développement connu

Configuration de référence communiquée :

- Windows 11 ;
- HP ProBook 640 G4 ;
- Intel Core i5-8250U ;
- 8 Go de RAM ;
- environnement de développement Windows.

Cette configuration impose une attention particulière à la mémoire, au nombre de polygones, aux textures, aux traitements vidéo et aux processus exécutés en parallèle.

### 3.4 Rendu Godot

Le renderer **Compatibility** (OpenGL) est recommandé pour privilégier la compatibilité et limiter la charge graphique. L'agent doit toutefois vérifier le renderer déjà configuré dans le projet avant de le modifier.

---

## 4. État connu du projet

**Le monde 3D de base est considéré comme déjà prêt par le porteur du projet.**

Conséquences pour les agents :

- ne pas repartir de zéro ;
- ne pas remplacer le monde existant par une nouvelle scène sans raison ;
- ne pas refaire les fondations 3D simplement par préférence personnelle ;
- inspecter les scènes, scripts, ressources et paramètres existants ;
- identifier les points d'intégration du système gestuel dans le monde actuel ;
- préserver les éléments visuels et fonctionnels déjà validés.

Le statut exact des autres modules doit être vérifié dans le dépôt local. Ce README décrit la cible convenue, pas une affirmation que chaque module est déjà implémenté.

---

## 5. Contrôle gestuel : principe central

### 5.1 Une seule main comme règle de conception

Le projet doit être pilotable principalement avec **une seule main**.

Le porteur du projet ne voit pas de situation normale qui justifie de rendre les deux mains obligatoires. Les agents ne doivent donc pas concevoir une navigation qui exige deux mains.

Une seconde main pourra éventuellement être exploitée dans une évolution future, mais aucune fonction essentielle du MVP ne doit en dépendre.

### 5.2 Gestes de déplacement avant et arrière

Le déplacement avant/arrière doit reproduire le principe du zoom tactile effectué dans l'air :

- **écarter le pouce et l'index** : commander l'avancement ;
- **rapprocher le pouce et l'index** : commander le recul.

La commande est calculée à partir de la distance entre le bout du pouce et le bout de l'index, détectés par MediaPipe.

Le comportement ne doit pas être un simple interrupteur brutal. Il doit être progressif, stable et réglable.

#### Comportement attendu

1. détecter la main ;
2. calculer la distance pouce-index ;
3. normaliser cette distance par rapport à une mesure de référence, par exemple la taille de la paume ;
4. appliquer un lissage pour réduire les tremblements ;
5. comparer la valeur à une référence calibrée ;
6. appliquer une zone morte autour de la valeur neutre ;
7. produire une commande de déplacement ;
8. limiter la vitesse maximale et éviter les accélérations soudaines.

La calibration doit permettre à l'utilisateur de définir sa position neutre. Une ouverture supérieure à la référence doit commander l'avant ; une fermeture inférieure doit commander l'arrière. Dans la zone neutre, le déplacement doit s'arrêter.

**Attention :** ne pas confondre l'ouverture naturelle de la main avec une commande permanente. Le système doit disposer d'une calibration, de seuils et d'une zone morte.

### 5.3 Orientation de la vue

L'utilisateur doit pouvoir regarder autour de lui en faisant glisser ou balayer sa main dans l'air, comme lorsqu'il fait glisser une vue 3D avec une souris.

Comportements attendus :

- mouvement horizontal de la main : rotation de la vue à gauche ou à droite (yaw) ;
- mouvement vertical : orientation vers le haut ou le bas (pitch) ;
- limitation de l'angle vertical pour éviter une rotation inconfortable ;
- lissage des mouvements ;
- sensibilité réglable.

Le geste doit être naturel et ne pas provoquer une rotation incontrôlée lorsque la main tremble.

### 5.4 Déplacement latéral

Le déplacement latéral gauche/droite peut être étudié, mais son geste définitif n'est pas encore figé. Il ne faut pas imposer une interprétation arbitraire.

L'agent peut proposer une expérimentation, comparer plusieurs gestes et demander validation avant de figer cette commande.

### 5.5 Un même geste peut avoir plusieurs fonctions selon le mode

Le pincement pouce-index peut servir à différentes actions, notamment :

- contrôler le déplacement avant/arrière ;
- sélectionner un objet ;
- cliquer sur un bouton ;
- valider une option.

Il faut donc éviter les conflits. Le système doit gérer des modes explicites et des transitions fiables.

---

## 6. Machine à états et prévention des conflits

Le moteur gestuel doit distinguer au minimum les états suivants :

| État | Rôle |
|---|---|
| `REST` | Main au repos, aucune commande active |
| `NAVIGATION` | Déplacement et orientation de la vue |
| `INTERACTION` | Ciblage, sélection et manipulation des objets |
| `CALIBRATION` | Mesure des références personnelles de l'utilisateur |
| `LEARNING` | Enregistrement d'exemples de gestes personnalisés |

Les noms peuvent être adaptés à la convention du code existant, mais les responsabilités doivent rester distinctes.

### 6.1 Règles de transition

- Le changement de mode doit être volontaire et reconnaissable.
- Un clic ne doit pas déclencher simultanément un déplacement.
- Un mouvement de navigation ne doit pas sélectionner accidentellement un équipement.
- Les actions doivent comporter un délai anti-répétition (*cooldown*) lorsque nécessaire.
- Les seuils doivent être configurables.
- La perte temporaire de la main doit entraîner un état sûr : arrêt du déplacement et absence de clic.
- Le retour de la main dans le champ doit être géré sans commande soudaine.

### 6.2 Robustesse

Prévoir :

- lissage des landmarks ;
- hystérésis autour des seuils ;
- zone morte ;
- seuil de confiance minimal ;
- temporisation des gestes ;
- gestion de la main perdue ;
- gestion d'une caméra indisponible ;
- bouton ou raccourci de recalibration pour les tests ;
- journalisation des états et des commandes pour le débogage.

---

## 7. Suivi de la main et Gesture Core

Le premier module gestuel à stabiliser est le **Gesture Core**.

### 7.1 Responsabilités

Le Gesture Core doit :

1. ouvrir la webcam ;
2. lire les images avec OpenCV ;
3. détecter une main avec MediaPipe ;
4. récupérer les 21 landmarks ;
5. calculer les distances et caractéristiques utiles ;
6. normaliser les données ;
7. lisser les mesures ;
8. déterminer l'état courant ;
9. reconnaître les gestes de base ;
10. exposer les résultats aux autres modules ;
11. transmettre les commandes à Godot lorsque le canal de communication sera intégré.

### 7.2 Premier livrable autonome

Avant de connecter le moteur gestuel à Godot, créer un programme Python autonome permettant de tester la webcam et les gestes.

L'outil de test doit afficher :

- le flux vidéo ;
- les points de repère de la main ;
- les connexions entre les points ;
- la distance pouce-index ;
- la valeur de référence calibrée ;
- le mode courant ;
- la commande reconnue : `ADVANCE`, `REVERSE` ou `STOP` ;
- le niveau de confiance ou un indicateur de validité lorsque disponible.

L'utilisateur doit pouvoir calibrer sa position neutre. Une touche de clavier peut être utilisée pour la calibration dans cet outil de développement uniquement. Cela ne signifie pas que le clavier sera nécessaire dans l'application finale.

### 7.3 Compatibilité MediaPipe

Les API MediaPipe Python peuvent varier selon la version installée. Avant d'écrire une intégration définitive :

- vérifier la version réelle de `mediapipe` ;
- vérifier l'API disponible ;
- ne pas mélanger les exemples de l'ancienne API `mp.solutions` et de l'API Tasks ;
- choisir une seule approche compatible avec l'environnement ;
- documenter les dépendances et les éventuels fichiers de modèle nécessaires.

Ne pas télécharger ou référencer un modèle externe sans vérifier sa provenance et sa compatibilité.

---

## 8. Personnalisation des gestes — Gesture Learning Studio

Une fonction importante du projet est la possibilité pour l'utilisateur de **nourrir le système avec ses propres mouvements**.

Il ne s'agit pas uniquement de choisir des gestes prédéfinis. L'application doit pouvoir évoluer vers un espace d'apprentissage où l'utilisateur enregistre ses propres exemples.

### 8.1 Parcours utilisateur cible

1. L'utilisateur ouvre le Gesture Learning Studio.
2. Il choisit « Enregistrer un nouveau geste ».
3. Il donne un nom ou un identifiant au geste, par exemple `OPEN_MAIN_MENU`.
4. L'application explique ou affiche le geste à effectuer.
5. L'utilisateur réalise le mouvement devant la webcam.
6. Le système enregistre les landmarks et les caractéristiques extraites.
7. L'utilisateur répète le geste plusieurs fois.
8. Les exemples sont sauvegardés avec leur étiquette.
9. Le classificateur est entraîné ou actualisé.
10. L'utilisateur teste le geste en direct.
11. Le système peut accepter, rejeter ou demander de nouveaux exemples.

### 8.2 Données à enregistrer

MediaPipe fournit 21 points de repère par main. Les données doivent être normalisées afin de limiter la dépendance :

- à la distance de la main par rapport à la caméra ;
- à la taille apparente de la main ;
- à la position de la main dans l'image ;
- aux petites variations de cadrage.

Privilégier des caractéristiques relatives à la paume et aux articulations plutôt que les coordonnées brutes de l'image.

### 8.3 Apprentissage recommandé pour le MVP

Pour les premiers gestes statiques personnalisés :

- commencer avec des règles géométriques pour les gestes simples ;
- envisager Scikit-learn (par exemple Random Forest ou SVM) pour les gestes appris ;
- conserver une classe `UNKNOWN` ou un mécanisme de rejet ;
- utiliser un seuil de confiance ;
- séparer les exemples d'entraînement et de test ;
- vérifier les gestes sur des exemples qui n'ont pas servi à l'entraînement.

Ne pas commencer par un réseau neuronal lourd ou un modèle temporel complexe.

Les gestes dynamiques pourront être étudiés ensuite avec des séquences de landmarks, DTW ou un modèle temporel léger, lorsque le besoin sera confirmé.

### 8.4 Sécurité fonctionnelle

Un geste inconnu ou peu fiable ne doit jamais déclencher une action importante. En cas de doute, le système doit ne rien faire et afficher un retour compréhensible.

---

## 9. Navigation Engine

Le Navigation Engine transforme les résultats du Gesture Core en commandes applicatives.

### Fonctions prévues

- déplacement avant ;
- déplacement arrière ;
- orientation horizontale ;
- orientation verticale ;
- arrêt immédiat en cas de perte de suivi ;
- calibration et sensibilité ;
- limites de vitesse et de rotation ;
- état de navigation clairement identifiable.

Le moteur doit être indépendant de MediaPipe : il reçoit des commandes normalisées et ne doit pas connaître les détails internes de la webcam.

Cette séparation permettra de remplacer ou d'améliorer le moteur de reconnaissance sans réécrire le contrôleur Godot.

---

## 10. Smart Targeting — Viseur et sélection

L'application doit comporter des viseurs permettant de cibler précisément les équipements et les éléments de l'environnement.

### 10.1 Viseur central

Un viseur central peut rester affiché dans le champ de vision. Il sert à indiquer l'endroit que l'utilisateur regarde ou cible.

### 10.2 Détection d'objet

Dans Godot, utiliser un mécanisme de raycasting adapté, par exemple `RayCast3D`, pour déterminer quel objet se trouve dans l'axe du viseur.

### 10.3 États visuels

Lorsqu'un objet est ciblé :

- afficher un contour lumineux ou un effet de surbrillance ;
- afficher son nom ou une fiche courte ;
- indiquer clairement qu'il est sélectionnable ;
- afficher les actions disponibles dans un menu contextuel.

### 10.4 Sélection

En mode interaction, un geste de pincement validé peut sélectionner l'objet ciblé.

La sélection doit être distincte du simple survol. Le système ne doit pas considérer chaque pincement de navigation comme un clic.

---

## 11. Spatial UI — Menus interactifs dans l'environnement

L'interface doit être intégrée visuellement au monde 3D. Les menus ne doivent pas être uniquement des fenêtres classiques séparées de l'environnement.

### 11.1 Menus contextuels flottants

Exemple : lorsque l'utilisateur cible un ordinateur, un menu apparaît à proximité de cet ordinateur.

Les options peuvent inclure :

- consulter les informations ;
- allumer ou éteindre ;
- déplacer l'équipement ;
- configurer l'adresse IP ;
- connecter un câble ;
- lancer un test ;
- ouvrir les propriétés.

Les options exactes dépendentront du type d'équipement.

### 11.2 Navigation dans les menus

L'utilisateur doit pouvoir :

- parcourir les options par un mouvement gestuel ;
- faire défiler une liste ;
- mettre une option en évidence ;
- sélectionner ou valider par un geste de pincement ;
- revenir en arrière ou fermer le menu par une action dédiée.

### 11.3 Principes ergonomiques

- menus lisibles et suffisamment grands ;
- peu d'options affichées simultanément ;
- état sélectionné clairement visible ;
- retours visuels et éventuellement sonores ;
- pas de menu qui masque complètement l'objet manipulé ;
- positionnement cohérent dans l'espace ;
- fermeture ou annulation toujours possible ;
- temps de maintien et cooldown pour éviter les clics accidentels.

### 11.4 Exemple de scénario

1. L'utilisateur regarde un ordinateur.
2. Le viseur le cible.
3. L'ordinateur est mis en évidence.
4. L'utilisateur effectue le geste de sélection.
5. Un menu apparaît près de l'ordinateur.
6. L'utilisateur fait défiler les options.
7. Il valide « Configurer l'adresse IP ».
8. Un panneau de configuration apparaît dans l'environnement.
9. Il renseigne les valeurs à l'aide du système d'interaction prévu.

La méthode exacte de saisie des valeurs doit être étudiée pour rester compatible avec une interaction à une seule main.

---

## 12. Manipulation des équipements et câblage

L'utilisateur doit pouvoir installer et manipuler des équipements dans les locaux virtuels.

### Équipements envisagés

- ordinateurs ;
- switches ;
- routeurs ;
- serveurs ;
- câbles Ethernet ;
- éventuellement prises, baies et autres éléments réseau utiles.

### Actions attendues

- sélectionner un équipement ;
- consulter ses propriétés ;
- ajouter ou installer un équipement ;
- positionner ou déplacer un équipement ;
- choisir un port ;
- connecter un câble à un port ;
- relier deux équipements ;
- retirer ou remplacer une connexion ;
- visualiser l'état de la liaison.

### Câblage

Le câble doit avoir une représentation visuelle compréhensible. Une connexion doit relier deux ports valides. Le système doit conserver les relations entre les équipements et leurs ports.

Ne pas se limiter à dessiner une ligne décorative : le câble doit représenter une vraie relation dans le modèle de simulation.

---

## 13. Simulation réseau pédagogique

La simulation réseau est d'abord interne à l'application et développée en C#.

### 13.1 Objectif

Permettre de comprendre les conséquences des connexions et des configurations sans exécuter une véritable infrastructure réseau virtuelle lourde.

### 13.2 Fonctionnalités progressives

- représentation des équipements et de leurs interfaces ;
- ports et liens ;
- adresses IP ;
- masque de sous-réseau ;
- passerelle ;
- état de l'interface ;
- vérification de compatibilité des adresses ;
- test de connectivité de type ping simulé ;
- résultat de réussite ou d'échec ;
- explication pédagogique de l'erreur ;
- visualisation simplifiée du chemin ou du déplacement des paquets.

### 13.3 Modèle de données conceptuel

Le modèle pourra comporter des objets tels que :

- `NetworkDevice` ;
- `ComputerDevice` ;
- `SwitchDevice` ;
- `RouterDevice` ;
- `ServerDevice` ;
- `NetworkInterface` ;
- `NetworkPort` ;
- `CableConnection` ;
- `IpConfiguration` ;
- `NetworkTopology` ;
- `ConnectivityTestResult`.

Ces noms sont des propositions de conception. Avant de les créer, l'agent doit vérifier les classes et conventions déjà présentes.

### 13.4 Règle importante

Une action dans l'environnement doit modifier l'état du modèle réseau. Par exemple, brancher un câble doit créer une connexion logique, et non uniquement une représentation graphique.

---

## 14. Missions et scénarios pédagogiques

Le projet doit pouvoir proposer des missions guidées.

Exemples convenus ou envisagés :

### Mission A — Installer un réseau de salle informatique

- installer un switch ;
- positionner plusieurs ordinateurs ;
- relier les équipements ;
- vérifier les connexions ;
- obtenir un réseau fonctionnel.

### Mission B — Configurer les adresses IP

- attribuer des adresses IP ;
- configurer les masques ;
- définir les passerelles nécessaires ;
- détecter les conflits ou incohérences ;
- tester la connectivité.

### Mission C — Dépanner un ordinateur isolé

- observer les symptômes ;
- inspecter les câbles ;
- vérifier l'état des ports ;
- vérifier les paramètres IP ;
- identifier et corriger la panne ;
- relancer un test.

### Mission D — Réseau de plusieurs départements

- concevoir une topologie pour plusieurs salles ou services ;
- installer les équipements nécessaires ;
- relier les segments ;
- configurer les éléments requis ;
- valider le fonctionnement global.

Les missions doivent expliquer les objectifs et fournir un retour pédagogique, pas seulement afficher un score.

---

## 15. Architecture logicielle cible

L'architecture doit séparer les responsabilités pour permettre aux agents de travailler sur des modules sans créer de dépendances inutiles.

### 15.1 Vue d'ensemble

```text
                         WEBCAM
                            |
                            v
                 +----------------------+
                 | Python / OpenCV      |
                 | Capture vidéo        |
                 +----------------------+
                            |
                            v
                 +----------------------+
                 | MediaPipe            |
                 | 21 hand landmarks    |
                 +----------------------+
                            |
                            v
                 +----------------------+
                 | Gesture Core         |
                 | Calibration          |
                 | Filtering             |
                 | Gesture Recognition  |
                 +----------------------+
                            |
                 commandes normalisées
                            |
                            v
                 +----------------------+
                 | UDP / localhost      |
                 | Messages structurés  |
                 +----------------------+
                            |
                            v
+---------------------------------------------------------+
|                    GODOT 4 .NET / C#                    |
|                                                         |
|  +------------------+     +---------------------------+ |
|  | Navigation       |     | Interaction Engine        | |
|  | Engine           |     | Modes / Selection         | |
|  +------------------+     +---------------------------+ |
|             |                         |                 |
|             v                         v                 |
|  +------------------+     +---------------------------+ |
|  | Player / Camera  |     | Smart Targeting           | |
|  +------------------+     +---------------------------+ |
|                                       |                 |
|                                       v                 |
|                          +---------------------------+  |
|                          | Spatial UI                |  |
|                          | Menus / Panels / Buttons  |  |
|                          +---------------------------+  |
|                                       |                 |
|                                       v                 |
|                          +---------------------------+  |
|                          | Network Simulation        |  |
|                          | Devices / Ports / Cables  |  |
|                          | IP / Connectivity         |  |
|                          +---------------------------+  |
+---------------------------------------------------------+

             Gesture Learning Studio
          (collecte, étiquetage, entraînement,
             validation des gestes personnels)
```

### 15.2 Organisation de dossiers indicative

L'organisation réelle du dépôt prime toujours. La structure suivante est une cible indicative, à adapter sans déplacer inutilement les fichiers existants.

```text
C:\NETVERSE-VR\
|
+-- NETVERSE-GODOT\
|   +-- Scenes\
|   |   +-- World\
|   |   +-- Rooms\
|   |   +-- Equipment\
|   |   +-- UI\
|   |
|   +-- Scripts\
|   |   +-- Player\
|   |   +-- Interaction\
|   |   +-- Navigation\
|   |   +-- Gestures\
|   |   +-- Network\
|   |   +-- Missions\
|   |
|   +-- Assets\
|   |   +-- Models\
|   |   +-- Textures\
|   |   +-- Materials\
|   |   +-- Audio\
|   |
|   +-- Data\
|   +-- project.godot
|
+-- gesture-engine\
|   +-- main.py
|   +-- camera.py
|   +-- hand_tracking.py
|   +-- gesture_recognition.py
|   +-- calibration.py
|   +-- filtering.py
|   +-- udp_sender.py
|   +-- learning\
|   +-- tests\
|   +-- requirements.txt
|
+-- documentation\
    +-- architecture\
    +-- decisions\
    +-- tests\
```

### 15.3 Principes de modularité

- Le suivi de la main ne doit pas contenir de logique Godot.
- Le Navigation Engine ne doit pas accéder directement à la webcam.
- Le système de menus ne doit pas dépendre de MediaPipe.
- Le système réseau ne doit pas dépendre du moteur gestuel.
- Les messages échangés doivent être documentés et versionnés.
- Les composants doivent être testables indépendamment.

---

## 16. Communication Python ↔ Godot

La communication locale doit transmettre des commandes simples et explicites.

### 16.1 Transport

UDP sur `127.0.0.1` est l'option retenue pour le premier prototype de communication. L'agent peut proposer une autre solution uniquement si un problème technique concret le justifie.

### 16.2 Exemple de message indicatif

```json
{
  "version": 1,
  "mode": "NAVIGATION",
  "hand_detected": true,
  "gesture": "PINCH_OPEN",
  "move_forward": 0.65,
  "move_backward": 0.0,
  "look_x": 0.12,
  "look_y": -0.04,
  "confidence": 0.94
}
```

Cet exemple illustre le contrat souhaité ; il ne doit pas être considéré comme un protocole déjà implémenté. Les noms définitifs doivent être stabilisés dans un document de protocole et utilisés de manière identique des deux côtés.

### 16.3 Exigences

- messages courts ;
- fréquence raisonnable ;
- valeurs normalisées ;
- version de protocole ;
- gestion des paquets manquants ;
- timeout si aucune donnée n'arrive ;
- arrêt des commandes si la main n'est plus détectée ;
- journalisation facultative pour le débogage.

---

## 17. Expérience visuelle et performances

La qualité visuelle est importante, mais elle ne doit pas compromettre la fluidité.

### Priorités

1. stabilité de l'application ;
2. fluidité de la caméra et de la navigation ;
3. lisibilité des équipements ;
4. feedback gestuel clair ;
5. menus compréhensibles ;
6. détails visuels supplémentaires.

### Recommandations

- privilégier des modèles low-poly ;
- limiter les textures très lourdes ;
- éviter les effets de post-traitement coûteux ;
- limiter les lumières dynamiques inutiles ;
- éviter de multiplier les processus Python ;
- éviter les copies d'images inutiles ;
- mesurer les performances sur la machine de référence ;
- prévoir une option de réduction de qualité graphique.

Le système doit rester utilisable sur un PC modeste. Ne pas optimiser uniquement sur une machine puissante.

---

## 18. Plan de développement prioritaire

Le monde 3D étant déjà considéré comme prêt, le travail doit maintenant avancer à partir de l'existant.

### Phase 0 — Audit du dépôt

Avant toute modification :

- identifier le dossier racine ;
- examiner les scènes Godot ;
- examiner les scripts C# ;
- vérifier la version Godot et .NET ;
- vérifier les dépendances Python ;
- vérifier le fonctionnement actuel du monde ;
- repérer les points d'intégration du joueur et de la caméra ;
- produire un bref état des lieux ;
- lister les risques et les fichiers qui ne doivent pas être écrasés.

### Phase 1 — Gesture Core autonome

Livrable :

- webcam opérationnelle ;
- détection d'une main ;
- affichage des 21 landmarks ;
- calcul de la distance pouce-index ;
- calibration ;
- lissage ;
- états `ADVANCE`, `REVERSE`, `STOP` ;
- arrêt sûr en cas de perte de la main.

**Critère de validation :** l'utilisateur peut ouvrir et fermer pouce-index devant la caméra et voir une commande stable, sans déclenchements excessifs.

### Phase 2 — Navigation gestuelle dans Godot

- stabiliser le protocole UDP ;
- recevoir les commandes dans Godot ;
- connecter l'avancement/recul à la caméra ou au contrôleur du joueur ;
- intégrer l'orientation de la vue ;
- tester les seuils et la sensibilité ;
- vérifier le comportement lorsque la main disparaît.

**Critère de validation :** l'utilisateur peut se déplacer dans le monde existant avec une seule main.

### Phase 3 — Smart Targeting

- viseur ;
- raycasting ;
- détection de l'équipement ciblé ;
- surbrillance ;
- informations contextuelles ;
- sélection par pincement en mode interaction.

### Phase 4 — Spatial UI

- menus contextuels flottants ;
- défilement gestuel ;
- sélection d'options ;
- panneaux de propriétés ;
- annulation et fermeture ;
- séparation stricte entre navigation et interaction.

### Phase 5 — Équipements et réseau simulé

- modèle logique des équipements ;
- ports ;
- câbles ;
- connexions ;
- configuration IP ;
- test de connectivité ;
- explication des erreurs.

### Phase 6 — Gesture Learning Studio

- enregistrement de gestes ;
- collecte d'exemples ;
- normalisation ;
- étiquetage ;
- entraînement léger ;
- test et rejet des gestes incertains.

### Phase 7 — Missions et finition

- scénarios guidés ;
- objectifs ;
- feedback pédagogique ;
- amélioration visuelle ;
- tests de performance ;
- préparation de la démonstration.

---

## 19. Périmètre MVP et priorisation

Le projet complet est ambitieux. Les agents doivent éviter de tenter de tout développer simultanément.

### MVP prioritaire

Le prototype démontrable doit, au minimum, permettre :

1. d'ouvrir le monde 3D existant ;
2. de détecter une main par webcam ;
3. de commander le déplacement avant/arrière avec l'écartement pouce-index ;
4. de regarder autour de soi avec un mouvement de la main ;
5. de cibler un équipement avec un viseur ;
6. d'afficher un menu contextuel ;
7. de sélectionner une action par geste ;
8. de réaliser une interaction réseau simple et visible.

Si le temps le permet, ajouter le branchement de deux équipements et un test de connectivité simulé.

### Évolutions ultérieures

- apprentissage personnalisé avancé ;
- bibliothèque de gestes ;
- davantage de missions ;
- topologies complexes ;
- animation détaillée des paquets ;
- assistance pédagogique intelligente ;
- compatibilité avec casque VR ;
- utilisation éventuelle de deux mains pour des opérations spécialisées, sans rendre cette option obligatoire.

---

## 20. Tests et critères d'acceptation

Chaque module doit avoir des critères de validation observables.

### Gesture Core

- la caméra peut être ouverte et libérée proprement ;
- la main est détectée dans des conditions normales ;
- les landmarks sont cohérents ;
- la distance pouce-index varie correctement ;
- la calibration est reproductible ;
- le bruit est filtré ;
- l'état neutre produit `STOP` ;
- la perte de la main produit un arrêt sûr.

### Navigation

- écarter les doigts produit l'avancement ;
- rapprocher les doigts produit le recul ;
- le déplacement s'arrête dans la zone neutre ;
- le mouvement est progressif ;
- le regard horizontal et vertical est contrôlable ;
- la caméra ne tremble pas excessivement ;
- la main perdue ne laisse pas le joueur continuer à avancer.

### Interaction et menus

- un objet ciblé est visuellement identifiable ;
- le menu correspond au bon objet ;
- une option survolée est visible ;
- le clic gestuel ne se déclenche qu'une fois ;
- navigation et sélection ne se déclenchent pas simultanément ;
- l'utilisateur peut annuler ou fermer un menu.

### Simulation réseau

- un câble relie deux ports valides ;
- les connexions sont conservées dans le modèle ;
- une configuration IP est enregistrée ;
- un test de connectivité produit un résultat cohérent ;
- une erreur est accompagnée d'une explication pédagogique.

### Performances

- pas de fuite de ressources évidente ;
- fermeture propre de la webcam ;
- pas de blocage de l'interface par le traitement vidéo ;
- fréquence d'images acceptable sur le PC de référence ;
- gestion des erreurs compréhensible.

---

## 21. Consignes obligatoires pour les agents IA travaillant en local

Cette section est particulièrement importante.

### 21.1 Avant de coder

1. Lire ce README intégralement.
2. Explorer le dépôt local.
3. Identifier ce qui fonctionne déjà.
4. Lire les fichiers concernés avant de les modifier.
5. Repérer les conventions de nommage et d'architecture.
6. Vérifier les versions et dépendances réelles.
7. Présenter brièvement le plan d'intervention avant une modification importante.

### 21.2 Pendant le développement

- travailler par petites étapes vérifiables ;
- modifier le minimum de fichiers nécessaire ;
- préserver les scènes et ressources existantes ;
- éviter les réécritures complètes non demandées ;
- ne pas ajouter de dépendance sans expliquer son utilité ;
- ne pas introduire de service cloud obligatoire ;
- ne pas ajouter de technologie lourde au MVP sans justification ;
- ne pas inventer des fonctionnalités déjà terminées ;
- ne pas masquer une erreur par un contournement fragile ;
- gérer les erreurs de caméra et de communication ;
- commenter les parties complexes, pas chaque ligne ;
- garder les responsabilités des modules séparées.

### 21.3 Après chaque étape

L'agent doit :

1. lancer les vérifications pertinentes ;
2. exécuter le programme ou les tests concernés lorsque possible ;
3. signaler précisément ce qui fonctionne ;
4. signaler ce qui n'a pas été testé ;
5. fournir les commandes pour reproduire le test ;
6. lister les fichiers créés ou modifiés ;
7. indiquer la prochaine étape logique.

### 21.4 Interdictions et précautions

Ne pas :

- supprimer ou remplacer le monde 3D existant sans accord ;
- convertir le projet en application web ;
- remplacer Godot par un framework JavaScript ;
- rendre le casque VR obligatoire ;
- rendre le smartphone obligatoire ;
- rendre une deuxième main obligatoire ;
- connecter directement la logique réseau au code de détection webcam ;
- déclencher des actions sur un geste incertain ;
- présenter une simulation réseau interne comme une véritable infrastructure réseau physique ;
- déclarer une tâche terminée sans l'avoir vérifiée.

### 21.5 Décisions à faire valider

Demander validation avant de figer :

- un nouveau geste de déplacement latéral ;
- une modification majeure de l'architecture ;
- le remplacement d'une technologie déjà utilisée ;
- une refonte du monde ou de son contrôleur ;
- une dépendance lourde ;
- un changement du protocole de communication ;
- une fonctionnalité qui rendrait les deux mains nécessaires.

---

## 22. Configuration Python indicative

L'environnement initialement envisagé utilise Python 3.11.

Exemple de création d'environnement virtuel sous Windows PowerShell :

```powershell
cd C:\NETVERSE-VR
mkdir gesture-engine
cd gesture-engine

py -3.11 -m venv .venv
.\.venv\Scripts\Activate.ps1

python -m pip install --upgrade pip
pip install mediapipe opencv-python
```

Vérifications :

```powershell
python --version
python -m pip --version
python -m pip show mediapipe
python -m pip show opencv-python
```

Ces commandes sont indicatives. Si l'environnement existe déjà, ne pas le recréer ni écraser ses dépendances sans inspection.

Il est recommandé de figer les versions testées dans `requirements.txt` une fois la compatibilité vérifiée.

---

## 23. Documentation attendue

Les agents doivent maintenir une documentation concise et à jour.

Fichiers recommandés :

- `README.md` : vision et instructions principales ;
- `ARCHITECTURE.md` : architecture réelle du dépôt ;
- `GESTURE_PROTOCOL.md` : messages Python ↔ Godot ;
- `GESTURE_DESIGN.md` : gestes, seuils, calibration et modes ;
- `NETWORK_SIMULATION.md` : modèle réseau et règles de simulation ;
- `CHANGELOG.md` : changements significatifs ;
- `TESTS.md` : procédures et résultats de validation.

Ne pas créer tous ces fichiers si le dépôt possède déjà une documentation équivalente. Mettre à jour les documents existants.

---

## 24. Résumé des décisions non négociables

1. Le projet s'appelle **NETVERSE VR**.
2. Le monde 3D existant est considéré comme prêt : le préserver.
3. Godot 4 .NET et C# constituent le socle de l'application 3D.
4. Python, OpenCV et MediaPipe assurent la vision gestuelle.
5. La webcam est le moyen de suivi de la main.
6. Le contrôle doit fonctionner avec une seule main.
7. Écarter pouce-index = avancer.
8. Rapprocher pouce-index = reculer.
9. Le déplacement doit être calibré, lissé et doté d'une zone morte.
10. Le mouvement de la main sert à orienter la vue.
11. Le déplacement latéral reste à expérimenter avant validation.
12. Les viseurs ciblent les objets dans le monde.
13. Un objet ciblé doit pouvoir afficher une surbrillance et un menu contextuel.
14. Les menus doivent être superposés visuellement à l'environnement 3D.
15. Les options doivent pouvoir être parcourues et sélectionnées par gestes.
16. La navigation et l'interaction doivent être séparées par des modes.
17. L'utilisateur doit pouvoir enregistrer ses propres gestes dans une évolution du système.
18. Les gestes incertains ne déclenchent aucune action.
19. Le réseau pédagogique est d'abord simulé en C#.
20. Le MVP doit rester compatible avec un PC modeste.
21. Le clavier, la souris, le smartphone et le casque VR ne sont pas requis pendant l'expérience gestuelle normale.
22. Les agents doivent auditer l'existant avant de coder et vérifier leurs modifications.

---

## 25. Première tâche recommandée à tout nouvel agent

Après avoir lu ce document, ne commence pas par réécrire l'application.

**Commence par un audit technique du dépôt local.**

Le rapport initial doit répondre à ces questions :

1. Où se trouve le projet Godot ?
2. Quelle version de Godot et de .NET est utilisée ?
3. Quelle scène constitue le monde principal ?
4. Quel script contrôle actuellement le joueur ou la caméra ?
5. Comment le monde 3D est-il organisé ?
6. Quels systèmes d'interaction existent déjà ?
7. Python est-il déjà configuré ?
8. Quelle version de MediaPipe est installée ?
9. Quelle webcam est détectée et accessible ?
10. Quel est le plus petit changement permettant de commencer le Gesture Core sans casser l'existant ?

Ensuite, proposer un plan court, puis implémenter **une seule étape à la fois**.

> **Objectif final :** faire de NETVERSE VR un laboratoire réseau 3D dans lequel un apprenant peut apprendre en agissant, avec une interaction gestuelle naturelle, principalement à une seule main, et un système capable d'apprendre progressivement les mouvements propres à chaque utilisateur.

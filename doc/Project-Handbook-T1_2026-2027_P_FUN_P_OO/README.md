# Projet avec XCL

_T1 2026-2027_

Bonjour,

Vous commencez un projet avec moi comme chef de projet.

Pour que notre collaboration soit le plus efficace possible, je vous communique à travers ce document certains points auxquels je tiens dans la manière de mener un projet.

## Méthodologie

Vous le savez, il existe plusieurs manières de gérer des projets. J'ai eu l'occasion d'expérimenter plusieurs méthodes au cours de ma carrière professionnelle. Aujourd'hui, je suis convaincu que la méthode la mieux adaptée au domaine du développement logiciel est l'**Agilité**.

Je me permets donc de rappeler ici les valeurs fondatrices de l'agilité:

> - Des personnes et leurs interactions plutôt que des outils et des processus
> - Des logiciels qui fonctionnent plutôt qu'une documentation exhaustive
> - La collaboration avec le client plutôt que la négociation contractuelle
> - L'adaptation au changement plutôt que le suivi d'un plan

Conformément à ces valeurs, je n'impose que très rarement des outils ou des pratiques. Tout ce qui suit est avant tout une proposition pour un contexte dans lequel je sais par expérience que les choses se déroulent habituellement très bien.

Si vous le souhaitez, vous pouvez mettre en œuvre d'autres outils ou pratiques. Il n'y a que deux conditions à cela :

1. Vous parvenez à me convaincre que votre choix sera bénéfique pour votre projet.
2. Je peux suivre votre projet sans que votre choix personnel ne crée pour moi de difficultés, blocage, travail supplémentaire , achat de licence,...

## Pratiques

### Analyse fonctionnelle

L'analyse fonctionnelle est réalisée au travers d'une liste de User Stories (US), comportant:

1. Un titre
2. Une description ("En tant que ... je veux ... pour ...")
3. Un ou plusieurs éléments graphiques (maquette, schéma, diagramme,…)
4. Un nombre significatif de tests d'acceptance

Pour plus de détails, voir [ce document](./files/sources/User%20Stories.md) et/ou [cet exemple](./files/Analyse%20Fonctionnelle%20-%20Un%20exemple.pdf).

### Rapport

Ce que vous réalisez est plus important que la documentation (voir valeurs agiles ci-dessus).  
Il n'est cependant pas possible de s'affranchir de toute documentation.

Les sections qui constituent votre rapport sont énumérées dans le cahier des charges de votre projet.

Le rapport doit être avant tout **utile**. Il ne doit contenir que des informations pertinentes. Le nombre de pages est sans importance, tant que les informations utiles y sont présentées proprement. À mes yeux, la qualité du rapport baisse quand on y ajoute du contenu pour occuper l'espace.

L'utilisation d'éléments graphiques (schéma d'architecture, diagrammes, maquettes, ...) est primordiale pour transmettre à un maximum d'informations.
Ces éléments doivent être présentés proprement. Faire attention à ce que les textes soient toujours lisibles.

### Journal de travail

La tenue d'un journal de travail est indispensable.

Votre journal de travail doit absolument satisfaire les critères suivants :

1. La structure et la présentation sont claires et soignées
1. Le résultat du travail (artefact) y est référencé précisément. Recommandation: mettre un lien sur le commit correspondant
1. La durée de chaque tâche est mentionnée. Si une tâche n'est pas achevée en fin de journée, son énoncé est suivi de `[WIP]`
1. Tout événement impactant le temps de travail prévu (activités ETML planifiées, absences, imprévus) y est reporté
1. Les heures supplémentaires y sont mentionnées
1. Les succès et les échecs sont mentionnés
1. Toute tâche de 45 minutes ou plus est commentée
1. Le temps de travail total par jour et sur l'ensemble du projet est calculé

### Daily Scrum

Votre projet est individuel ? Vous trouvez peut-être que le Daily Scrum n'a donc pas de sens (certains pensent qu'on ne fait pas une mêlée avec une seule personne!).  
Je vous recommande pourtant vivement de conserver cette pratique malgré tout. Consacrez les premières minutes de chaque journée de travail à prendre un peu de recul et faire le point.

- Qu'est-ce que j'ai fait hier ? (l'occasion de vérifier que votre journal de travail est à jour)
- Qu'est-ce que je pense faire aujourd'hui ?
- Est-ce que j'ai un problème qui me freine ou me bloque ? (l'occasion de planifier un moment d'aide avec un tiers)

## Outils

Un outil est quelque chose qui nous aide à réaliser un travail plus efficacement. On creuse plus facilement un trou dans le sol avec une pelle qu'avec une cuillère à café.  
Je liste donc ici des outils informatiques qui - j'en suis convaincu - vous aident à réaliser votre projet.

Je suis prêt à discuter et à m'adapter si vous souhaitez faire usage d'autres outils que ceux ci-dessous, mais attention: il faudra savoir me convaincre que vous serez plus performants avec l'outil que vous proposez.

### Journal de travail

Je recommande fortement l'utilisation de GitJournal, qui se trouve dans le standard-toolset ETML et qui tire profit des efforts que vous avez fait dans la gestion de vos commits pour générer automatiquement la grande majorité de votre journal.

### Contrôle de version: GitHub

Sur ce point-là, vous n'avez pas le choix. Il est important que l'ensemble des travaux de la classe que j'accompagne se retrouve dans un contexte commun.

- Créez un repo privé dans votre compte et invitez-moi. Donnez-moi des droits d'écriture, car il est possible que je vous donne certains retours directement dans votre dépôt.
- Suivez la convention de nommage des commits décrite dans les [conventions techniques](https://eduvaud.sharepoint.com/:b:/r/sites/ETML_INF_ALL_Teams/Supports%20de%20cours/Organisation/Normes-Conventions/ConventionsTechniques-1.0.0.pdf?d=wc9492e811e9f4d40b6519ea0c6f58283&csf=1&web=1&e=ygynOH) de l'ETML.
- Appliquez le modèle [gitflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) pour la gestion des branches, avec les branches `main`, `develop` et les branches de features.
- Tous les fichiers utiles au travail et autres que du code (documentation technique interne ou externe, rapports, exemples, snippets, etc...) se trouvent dans l'arborescence sous `doc`, dont la structure est livrée à votre bon sens.
- Bien que cela soit une pratique controversée, je demande à ce que les fichiers concernant la documentation (notamment le rapport et le journal de travail) soient également intégrés dans le repository, dans le dossier `doc`.

### Gestion de projet: GitHub Project

A tout moment, je dois pouvoir savoir ce qui a été fait, ce qui reste à faire et ce que vous êtes en train de faire dans votre projet.

Les projets sous forme de Kanban dans Github sont particulièrement bien adapté pour cela.

Créez un projet, lié à votre repository, en vous basant sur le modèle `Kanban` proposé.  
Ne conservez que 5 colonnes: `Sandbox`, `Backlog`, `In Progress`, `In Review`, `Done`  
Invitez-moi dans le projet.

Rédigez vos US sous forme d'issues dans la SandBox et sollicitez-moi. Nous discutons, corrigeons, détaillons, précisons et finalement validons vos stories en les passant dans le backlog.

Vous estimez et planifiez vos stories vous-même.

### Intelligence Artificielle

Dans le cadre de nos projets actuels, vous pouvez utiliser l'IA dans le mode "Appui ciblé".

Cela signifie que vous pouvez l'utiliser de manière ponctuelle lorsque vous êtes bloqué sur une chose à créer ou un bug à corriger.

Vous avez l'obligation de comprendre les propositions ou solutions que l'IA vous fournit. Constater le bon fonctionnement du code n'est pas suffisant.

À tout moment, je peux vous demander de m'expliquer des parties de code que vous avez commité dans votre repo. Vous devez être capable de le faire de manière convaincante.

Qu'est-ce que ça veut dire 'convaincant' ?  
Exemple avec le code suivant:

```csharp
pos = Math.Clamp(pos, 0, Screen.Width - ship.Width);
```

Explication non convaincante:

> On envoie les valeurs positions, largeur d'écran et largeur du vaisseau à la méthode Clamp et on stocke le résultat dans la variable

Explication convaincante :

> On assure que le vaisseau ne sort pas de l'écran ni à gauche ni à droite. Clamp ça retourne une valeur qui est entre les deux bornes. Ici c'est entre zéro pour le bord gauche et la largeur de l'écran pour le bord droite. On enlève encore la largeur du vaisseau à droite, parce que si la coordonnée du vaisseau est tout à droite, il va être dessiné en dehors de l'écran.

Dans le rapport, il vous est demandé d'écrire une section décrivant, l'usage que vous avez fait de l'IA.

Cette section doit être bien détaillée:

- Quelle(s) IA avez-vous utilisé ?
- De quelle manière ? (navigateur, plug-in IDE, agent, API, ...)
- Donnez des exemples concrets: prompt, réponse, mise en place, résultat, correction, nombre de token, ...

Attention : ne dites **PAS** ce que vous n'avez **PAS** fait avec l'IA. Cela ne m'intéresse **PAS**.

## Livraison

Rappel: Le cahier des charges du projet définit les livrables.

Chaque livraison est effectuée au moyen d'une [release github](https://docs.github.com/en/repositories/releasing-projects-on-github/managing-releases-in-a-repository)

Le rapport dans son état présent et le journal de travail à jour doivent être ajoutés en PDF aux assets de la release.

Dans le cas où un livrable ne peut pas être directement inclus dans le repository (taille trop grande par exemple), joignez à la release un fichier `pdf` qui contient un lien vers le livrable. Assurez-vous que la cible du lien soit accessible par votre destinataire.

Toute livraison doit m'être notifiée explicitement par un email contenant un lien sur la release.

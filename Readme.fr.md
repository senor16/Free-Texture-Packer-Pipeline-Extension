# Free Texture Packer Pipeline

Une extension du Monogame content pipeline pour charger les spritesheets générées par Free Texture Packer dans Monogame

*Lire dans une autre langue : [English](Readme.md), [Français](Readme.fr.md)*

## Sommaire
**[Projet d'exemple](#projet-dexemple)**  
[Prérequis](#prérequis)  
[Installation](#installation)  
[Utiliser Free Texture Packer pour créer une spritesheet](#utiliser-free-texture-packer-pour-créer-une-spritesheet)  
[Ajout de l'extension au content pipeline](#ajout-de-lextension-au-content-pipeline)  
[Chargement de la spritesheet dans un projet monogame](#chargement-de-la-spritesheet-dans-un-projet-monogame)  
**[Contribution](#contribution)**  
**[Versioning](#versioning)**  
**[Auteurs](#auteurs)**  
**[License](#license)**  
**[Remerciements](#remerciements)**  

## Projet d'exemple

### Prérequis
Vous devez avoir Monogame instalé   
Pour cet exemple, j'utilise `Monogame 3.8.1.303`  
Vous devez aussi avoir Free Texture Packer instalé  
Pour cet exemple, j'utilise `Free Texture Packer 0.6.7`

### Installation
Il vous faut deux paquets nuget : `FreeTexturePackerReader` et `FreeTexturePackerPipeline`  

    dotnet add package FreeTexturePackerReader --version 1.0.0
    dotnet add package FreeTexturePackerPipeline --version 1.0.0

### Utiliser Free Texture Packer pour créer une spritesheet
1. Ouvrez Free Texture Packer
2. Ajouter les images contnus dans le dossier `images`
3. Nommez la texture `chess`
4. Selectionner le format `png`
5. Cochez la case `Remove file ext`
7. Choisissez le format `custom`
8. Cliquez sur le crayon juste à côté
9. Coller ce template d'export:

        {
            "name":"{{{config.imageName}}}",
            "texture":"{{{config.imageName}}}.png",
            "frames":[
                {{#rects}}
                {
                "filename":"{{{name}}}",
                "frame":{
                    "X":{{frame.x}},
                    "Y":{{frame.y}},
                    "Width":{{frame.w}},
                    "Height":{{frame.h}}
                },
                "rotated":{{rotated}},
                "trimmed":{{trimmed}},
                "spriteSourceSize":{
                    "X":{{spriteSourceSize.x}},
                    "Y":{{spriteSourceSize.y}},
                    "Width":{{spriteSourceSize.w}},
                    "Height":{{spriteSourceSize.h}}
                },
                "sourceSize":{
                    "Width":{{sourceSize.w}},
                    "Height":{{sourceSize.h}}
                },
                "pivot":{
                    "X":0.5,
                    "Y":0.5
                }        
                }{{^last}},{{/last}}
                {{/rects}}
                ],
            "meta":{
                "app":"{{{appInfo.url}}}",
                "version":"{{{appInfo.version}}}",
                "image":"{{{config.imageName}}}",
                "format":"{{{config.format}}}",
                "size":{
                    "Width":{{config.imageWidth}},
                    "Height":{{config.imageHeight}}
                },
                "scale":{{config.scale}}
            }
        }
10. Cochez la case `Allow trim` et `Allow rotation`
11. Ecrivez `spritesheet` comme extension
12. Cliquez sur  `Save`
13. Cliquez sur `Export`
14. Choisissez un dossier et cliquez sur `Select Folder`

### Ajout de l'extension au content pipeline
1. Créez un projet Monogame pour desktop
2. Ajouter l'extension du pipeline dans votre projet (voir [Installation](#installation))
3. Ouvrez `Content.mgcb`
4. Cliquez sur `Content`
5. Sous `Properties`, défilez jusqu'à `Reference` et cliquez dessus
6. Cliquez `Add`
7. Selectionner le fichier .dll correspondant à l'extension
    Vous le trouverez à la racine de votre projet dans le dossier `\bin\Debug\net6.0\FreeTexturePackerPipeline.dll`
8. Cliquez sur `Ok`

### Chargement de la spritesheet dans un projet monogame
1. Copiez les fichiers quevous venez d'exporter :`chess.png` et `chess.spritesheet` (voir [Utiliser Free Texture Packer pour créer une spritesheet](#utiliser-free-texture-packer-pour-créer-une-spritesheet)) dans le dossier Content de votre projet
2. Click droit sur `Content` puis `Add`>`Existing Item...` 
3. Selectionnez `chess.spritesheet` puis cliquez sur `Open`
4. Allez à `Build`>`Build` ou appuyez sur la touche `F6`
5. Ouvrez votre fichier `Game1.cs` et ajouter ceci tout en haut 

        using FreeTexturePackerReader;
6. Charger la spritesheet  
    Dans la méthode `LoadContent` ajoutez: 

        chess = Content.Load<SpriteSheet>("chess");
7. Afficher une spriteà l'écran  
    Dans la méthode `Draw` ajoutez:   

        _spriteBatch.Begin();
        var sprite = chess.frames["reine_blanc"];
        _spriteBatch.Draw(chess.Texture,new Vector2(100,100),sprite.sourceRect,Color.White,sprite.rotation,sprite.origin,1,SpriteEffects.None,1);
        _spriteBatch.End();
8. Lancer le jeu

Vous trouverez le projet d'exemple dans ledossier `SampleGame`  

## Contribution

Veuillez lire [CODE_DE_CONDUITE.md](CODE_DE_CONDUITE.md) pour des détails sur notre code de conduite et le processus pour nous soumettre des pull request

## Versioning

Nous utilisons [SemVer](http://semver.org/) pour le versionning.

## Auteurs

* **Sesso Kosga** - *Travail  initial* - [senor16](https://github.com/senor16)

## License

Ce projet est publié sous la licence MIT - voir [licence.md](licence.md) pour plus de détails

## Remerciements

Mes remerciements vont à :
* [Ragath](https://github.com/Ragath) pour m'avoir suggéré d'implémenter [la librairie que j'avais faite](https://github.com/senor16/Free-Texture-Packer-Loader) en cette extension du Monogame content pipeline  
    Vous m'avez fait découvrir un aspect du framework monogame que je ne conaissais pas
* **Matt Weber** Pour son tutoriel sur l'extension du content pipeline :  [How to extend Monogame's content pipeline](https://badecho.com/index.php/2022/08/17/extending-pipeline/)  
    Votre tutoriel m'a permis d'approfondir ma connaissance du content pipeline
* **RB Whitaker's Wiki** pour son tutoriel sur la création d'une extension pour le content pipeline :  [Building a content pipeline extension](https://rbwhitaker.com/tutorials/xna/content-pipeline/extending/part-1/)  
    Votre tutoriel m'a aidé à resoudre un bug qui me causais des mauts de têtes

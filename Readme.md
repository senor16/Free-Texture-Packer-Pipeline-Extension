# Free Texture Packer Pipeline Extension

A Monogame content pipeline extension to load spritesheets generated from [Free Texture Packer](https://free-tex-packer.com) into [Monogame](https://monogame.net)

*Read in another language : [English](Readme.md), [Français](Readme.fr.md)*

## Summary
**[Getting started](#getting-started)**  
[Prerequisites](#prerequisites)  
[Installing](#installing)  
[Using Free Texture Packer to create a spritesheet](#using-free-texture-packer-to-create-a-spritesheet)  
[Adding the extension to the content pipeline](#adding-the-extension-to-the-content-pipeline)  
[Loading the spritesheet and using it in a monogame project](#loading-the-spritesheet-and-using-it-in-a-monogame-project)
**[Contributing](#contributing)**  
**[Versioning](#versioning)**  
**[Authors](#authors)**  
**[License](#license)**  
**[Acknowledgments](#acknowledgments)**  

## Getting started 

### Prerequisites
You must have Monogame installed.  
For this example, I'm using `Monogame 3.8.1.303`  
You must also have Free Texture Packer installed  
For this example, I'm using `Free Texture Packer 0.6.7`  

### Installing
You need two nuget packages : `FreeTexturePackerReader` and `FreeTexturePackerPipeline`  

    dotnet add package FreeTexturePackerReader --version 1.0.0
    dotnet add package FreeTexturePackerPipeline --version 1.0.0

### Using Free Texture Packer to create a spritesheet
1. Open Free Texture Packer
2. Add the images in the `images` folder
3. Set texture name to `chess`
4. Set texture format to `png`
5. Check the `Remove file ext` box
7. For the format, choose `custom`
8. Click the little pencil next to it
9. Paste the following export template :

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
10. Check `Allow trim` and `Allow rotation` boxes
11. For file extension, write `spritesheet`
12. Click `Save`
13. Click `Export`
14. Choose a folder and click `Select Folder`

### Adding the extension to the content pipeline
1. Create a monogame desktop project
2. Add the pipeline extension to your project (see [Installing](#installing))
3. Open `Content.mgcb`
4. Click on `Content`
5. Under `Properties`, scroll down to `Reference` and click it
6. Click `Add`
7. Locate the .dll for the extension pipeline.  
    You'll find it from the root of you game project in the folder `\bin\Debug\net6.0\FreeTexturePackerPipeline.dll`
8. Click `Ok`

### Loading the spritesheet and using it in a monogame project
1. Copy the spritesheet files your exported :`chess.png` and `chess.spritesheet` (see [Using Free Texture Packer to create a spritesheet](#using-free-texture-packer-to-create-a-spritesheet)) into the Content folder of your game project
2. Right click on `Content` then `Add`>`Existing Item...` 
3. Select `chess.spritesheet` then click `Open`
4. Got to `Build`>`Build` or press `F6`
5. Open your `Game.cs` and add this using at the top  

        using FreeTexturePackerReader;
6. Load the spritesheet  
    In the `LoadContent` method, add : 

        chess = Content.Load<SpriteSheet>("chess");
7. Draw a sprite at the screen  
    In the `Draw` method, add : 

        _spriteBatch.Begin();
        var sprite = chess.frames["reine_blanc"];
        _spriteBatch.Draw(chess.Texture,new Vector2(100,100),sprite.sourceRect,Color.White,sprite.rotation,sprite.origin,1,SpriteEffects.None,1);
        _spriteBatch.End();
8. Run the game  

You'll find the sample project in the `SampleGame` folder

## Contributing

Please read [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. 

## Authors

* **Sesso Kosga** - *Initial work* - [sessokosga](https://github.com/sessokosga)

## License

This project is licensed under the MIT License - see the [licence.md](licence.md) file for details

## Acknowledgments

Thanks to :
* [Ragath](https://github.com/Ragath) for his suggestion to implement the [lib I had made](https://github.com/senor16/Free-Texture-Packer-Loader) into this Monogame Pipeline extension  
    You made me discover a part of the monogame framework that I did'nt knew.
* **Matt Weber** for his toturial on [How to extend Monogame's content pipeline](https://badecho.com/index.php/2022/08/17/extending-pipeline/)  
    Your tutorial helped me to deepen my understanding of the monogame content pipeline
* **RB Whitaker's Wiki** for his tutorial on [Building a content pipeline extension](https://rbwhitaker.com/tutorials/xna/content-pipeline/extending/part-1/)  
    Your tutorial helped to fix a bug that has caused me terrible headache

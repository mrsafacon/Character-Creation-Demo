# Character Creation Demo
Customizable character creation screen in Unity.

 #### *All Code, Art Assets, Fonts by [Safacon](https://www.safacon.com) (Omar Lopez)*

![Preview Gif](/ReadmeImages/FullPreview.gif)

## Adding Customizable Options
All options are loaded at runtime from the "Resources/Database" folder. The system handles generating the In-game content and preview/selection buttons based on that is loaded. This allows importing assets to be simple and straight-forward.

Create the DatabaseObject using the context menu ( Create > CustomizableCharacterObject > ... )
Make sure is in the appropriate subfolder (eg. "Resources/Database/Colors" for color options)

![Object Creation Image](/ReadmeImages/DatabaseObjectCreation.png)

Then assign the values in the inspector

![Inspector Image](/ReadmeImages/DatabaseValueAssignment.png)

Now the assets are ready to be processed at runtime and will be available as options in-game.

## Core Scripts

#### RuntimeDatabase.cs
- Loads and stores all the customization options at start time.
- Provides public methods for retrieving database information.

#### CustomizableCharacter.cs
- Applies changes to the character in 3D space.
- Provides public methods for applying changes.
- Provides listener callbacks for any changes made.
- Handles appearance randomization.

## UI Scripts

#### SelectionPanel.cs & ColorPanel.cs
- Generate buttons for each avalable appearance change.
#### SelectionHandlers
- Processes button presses.
- Manages highlighting active selection utilizing CustomizableCharacter's listener callbacks.

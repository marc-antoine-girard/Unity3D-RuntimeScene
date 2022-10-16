<p align="center">
    <img alt="GitHub package.json version" src ="https://img.shields.io/github/package-json/v/marc-antoine-girard/Unity3D-RuntimeScene" />
    <a href="https://openupm.com/packages/com.marc-antoine-girard.runtimescene/">
        <img src="https://img.shields.io/npm/v/com.marc-antoine-girard.runtimescene?label=openupm&registry_uri=https://package.openupm.com" />
    </a>
    <br>
    <a href="https://github.com/marc-antoine-girard/Unity3D-RuntimeScene/issues">
        <img alt="GitHub issues" src ="https://img.shields.io/github/issues/marc-antoine-girard/Unity3D-RuntimeScene" />
    </a>
    <a href="https://github.com/marc-antoine-girard/Unity3D-RuntimeScene/pulls">
        <img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr/marc-antoine-girard/Unity3D-RuntimeScene" />
    </a>
    <img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/marc-antoine-girard/Unity3D-RuntimeScene" />
    <a href="https://github.com/marc-antoine-girard/Unity3D-RuntimeScene/blob/main/LICENSE.md">
        <img alt="GitHub license" src ="https://img.shields.io/github/license/marc-antoine-girard/Unity3D-RuntimeScene" />
    </a>
    <a href="https://www.codacy.com/gh/marc-antoine-girard/Unity3D-RuntimeScene/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=marc-antoine-girard/Unity3D-RuntimeScene&amp;utm_campaign=Badge_Grade"><img src="https://app.codacy.com/project/badge/Grade/bfb6566973e14907a06ec82ea35016ea"/>
    </a>

</p>

# Unity3D-RuntimeScene

A simple class that lets you reference scenes in the Editor.

## Summary

-   Allows referencing scenes in Unity's Inspector
-   Avoids scene name conflicts when using RuntimeScene methods
-   The resulting RuntimeScene instances in build are super lightweight
-   In the editor, allows loading scene not in Build Settings
-   Add or remove scenes from Build Settings using the Context Menu

## Installation

### Using git

-   In Window -> Package Manager -> Add package from git URL...
-   Paste `https://github.com/marc-antoine-girard/Unity3D-RuntimeScene.git`

### Using OpenUPM

The package is available on the [openupm registry](https://openupm.com). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```curl
openupm add com.marc-antoine-girard.runtimescene
```

## Usage

> **Note** If you're using Assemblies, don't forget to reference `ShackLab.RuntimeScene`

Here's a simple example on how to use RuntimeScene:

```csharp
public class LoadScene : MonoBehaviour
{
    public RuntimeScene scene;
    void LoadingMethods()
    {
        // Load scene synchronously 
        scene.LoadScene();
        // Load scene asynchronously 
        scene.LoadSceneAsync();
    }
}
```

RuntimeScene has many overloaded methods, most mirroring `SceneManager.LoadScene` and `SceneManager.LoadSceneAsync`.

![image](https://user-images.githubusercontent.com/62125329/185726016-3e3b8e08-9649-4c7e-8758-21e6ae85f3de.png)

You can also use `SceneManager`'s methods to load scenes, but it is **NOT RECOMMENDED**.

The biggest advantages of using RuntimeScene's methods over SceneManager are:

-   In Build, RuntimeScene uses the build Index by default instead of the scene's name, which avoids unnexpected behaviour when [Build Settings contains multiple scenes with the same name](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html#:~:text=The%20given%20sceneName,the%20full%20path.)

-   In the Editor, Scenes will load **even if not in the Build Settings.**

    -   The intent behind this decision is to allow faster testing in some situation.

    -   Note that loading a scene that is not in the Build Settings will throw a warning in the Editor, letting you know this won't work in build.

    -   If you want to opt-out of this feature, you can define "**DISABLE_LOAD_EDITOR**" `Edit -> Project Settings -> Player -> Other Settings -> Scripting Define Symbols`

        > **Warning** | Scene {scene name} is not in the build settings. Consider adding it if you plan on using it in build

### Editor Tools

You will also get a warning box under the RuntimeScene when referencing a Scene that is not in Build Settings
![image](https://user-images.githubusercontent.com/62125329/185725959-067f4c64-eb16-44a8-a4af-bfc9334717db.png)

You can quickly add or remove the Scene using the Context Menu (right-click):

![image](https://user-images.githubusercontent.com/62125329/185725977-e1b07dc2-e92a-4abe-926a-f000590b598f.png)![image](https://user-images.githubusercontent.com/62125329/185725988-7b5e7148-c808-49b0-ae51-0ec30d28c99c.png)

### RuntimeScene Methods

```csharp
public void LoadScene();
public void LoadScene(LoadSceneMode mode);
public void LoadScene(LoadSceneParameters parameters);

public AsyncOperation LoadSceneAsync(bool allowSceneActivation = true);
public AsyncOperation LoadSceneAsync(LoadSceneMode mode, bool allowSceneActivation = true);
public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters, bool allowSceneActivation = true);
```

* * *

### Addressables

When using scenes with Addressables, you can use `AssetReferenceScene`.

> **Note** AssetReferenceScene is only available when Addressables is in the project.

```csharp
public class LoadScene : MonoBehaviour
{
    public AssetReferenceScene scene;
    void Start()
    {
        scene.LoadSceneAsync();
    }
}
```

## Contributions

Pull requests are welcomed. Please feel free to fix any issues you find, or add new features.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/R6R6EBROQ)

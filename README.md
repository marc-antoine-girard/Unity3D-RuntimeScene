<p align="center">
    <img alt="GitHub package.json version" src ="https://img.shields.io/github/package-json/v/marc-antoine-girard/Unity3D-RuntimeScene" />
    <a href="https://github.com/marc-antoine-girard/Unity3D-RuntimeScene/issues">
        <img alt="GitHub issues" src ="https://img.shields.io/github/issues/marc-antoine-girard/Unity3D-RuntimeScene" />
    </a>
    <a href="https://github.com/marc-antoine-girard/Unity3D-RuntimeScene/pulls">
        <img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr/marc-antoine-girard/Unity3D-RuntimeScene" />
    </a>
    <a href="https://github.com/marc-antoine-girard/Unity3D-RuntimeScene/blob/main/LICENSE.md">
        <img alt="GitHub license" src ="https://img.shields.io/github/license/marc-antoine-girard/Unity3D-RuntimeScene" />
    </a>
    <img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/marc-antoine-girard/Unity3D-RuntimeScene" />
</p>

# Unity3D-RuntimeScene

A simple class that lets you reference scenes in the Editor.

Main features:

- Allows referencing scenes in Unity's Inspector
  
- Avoids scene name conflicts when using RuntimeScene methods
  
- The resulting RuntimeScene instances in build are super lightweight
  
- In the editor, allows loading scene not in Build Settings
  
- Add or remove scenes from Build Settings using the Context Menu
  

## Installation

### Using git

- In Window -> Package Manager -> Add package from git URL...
- Paste `https://github.com/marc-antoine-girard/Unity3D-RuntimeScene.git`

### Using OpenUPM

## Usage

**Note**: If you're using Assemblies, don't forget to reference `ShackLab.RuntimeScene`

Here's a simple example on how to use RuntimeScene. Each methods in RuntimeScene have many overloads, most mirroring `SceneManager.LoadScene` and `SceneManager.LoadSceneAsync`.

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

You can also use `SceneManager`, but it is not recommended.

```csharp
public class LoadScene : MonoBehaviour
{
    public RuntimeScene scene;
    void Start()
    {
        // Load by name
        SceneManager.LoadScene(scene.Name);

        // Load by build index
        SceneManager.LoadScene(scene.BuildIndex);
    }
}
```

The biggest advantages of using RuntimeScene's methods over SceneManager are:

- It uses the build Index instead of scene name, which avoids unnexpected behaviour when [Build Settings contains multiple scenes with the same name](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html#:~:text=The%20given%20sceneName,the%20full%20path.)
  
- In the editor, it will load the scene **even if it is not in the Build Settings**
  
  - The intent behind this decision is to allow faster testing in some situation.
    
  - Note that loading a scene that is not in the Build Settings will throw a warning in the Editor, letting you know this won't work in build.
    
  
  > Scene {scene name} is not in the build settings. Consider adding it if you plan on using it in build
  

You will get a warning if RuntimeScene is referencing a Scene not in Build Settings

(image here)

You can quickly add or remove the Scene using the Context Menu (right-click):

(image here)

## RuntimeScene Methods

```csharp
public void LoadScene();
public void LoadScene(LoadSceneMode mode);
public void LoadScene(LoadSceneParameters parameters);

public AsyncOperation LoadSceneAsync(bool allowSceneActivation = true);
public AsyncOperation LoadSceneAsync(LoadSceneMode mode, bool allowSceneActivation = true);
public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters, bool allowSceneActivation = true);
```

## Contributions

Pull requests are welcomed. Please feel free to fix any issues you find, or add new features.
# Unity3D-RuntimeScene
A simple class that lets you reference scenes in the Editor.

## Usage
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

## Contributions
Pull requests are welcomed. Please feel free to fix any issues you find, or add new features.

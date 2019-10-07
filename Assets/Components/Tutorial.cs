using UnityEngine;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour {
    protected GameController gameController;

    protected void Awake() {
        gameController = GetComponent<GameController>();
    }

    protected void Start() {
        gameController.messageBox.Write("Welcome to Black Hole: Fusion!", Step1);
    }

    public void Step1() {
        gameController.messageBox.Write("Press Left Mouse Button to spawn your Black Hole!", null);
    }

    public void Step2() {
        gameController.messageBox.Write("Click outside the Black Hole to spawn a Particle!", Step3);
    }
    
    public void Step3() {
        gameController.messageBox.Write("6 Particles of the same size merge into a bigger one!", Step4);
    }

    public void Step4() {
        gameController.messageBox.Write("If a Particle touches the Black Hole, it gets eaten!", Step45);
    }

    public void Step45() {        
        gameController.messageBox.Write("Zoom in and out with the Mouse Scroll Wheel!", Step5);
    }

    public void Step5() {        
        gameController.messageBox.Write("Pan the camera with Middle Mouse Button (or Space)!", Step6);
    }
    
    public void Step6() {        
        gameController.messageBox.Write("Larger Particles automatically generate smaller particles!", Step7);
    }

    public void Step7() {        
        gameController.messageBox.Write("You can press Tab to buy upgrades at my Mass Shop!", Step8);
    }

    public void Step8() {
        gameController.messageBox.Write("Grow the Black Hole the most you can!", Step9);
    }
    
    public void Step9() {
        gameController.messageBox.Write("Have fun!", null);
    }
}
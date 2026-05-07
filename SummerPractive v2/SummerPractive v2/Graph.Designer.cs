public class Graph : Form
{
    private FunctionGraph graphControl;

    public Graph()
    {
        Text = "График функции";
        Size = new Size(1920, 1080);
        graphControl = new FunctionGraph();
        graphControl.Dock = DockStyle.Fill;
        Controls.Add(graphControl);
    }
}

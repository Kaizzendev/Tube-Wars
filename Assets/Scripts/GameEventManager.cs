using System;

public static class GameEventManager
{
    public static Action<Node.Node, int , int> onChangeLeader;
    public static Action<Node.Node> onNodeSelected;
}
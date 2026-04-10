using System.Collections.Generic;
using AIUtility;

public class Team
{
    public int id;
    public List<Node.Node> ownedNodes;
    public int totalUnits;
    public UtilitySystemBrain brain;
}
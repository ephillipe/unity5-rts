using UnityEngine;

public enum ResourceType
{
	Gold,
	Wood,
	Food
}

public class ResourceRoot : MonoBehaviour {

	public ResourceType resource;
	[SerializeField]
	private int resourcesLeft;
	public delegate void EmptySourceEventHandler ();
	public event EmptySourceEventHandler OnEmptySource;

	public bool IsEmpty 
	{
		get { return resourcesLeft <= 0; }
	}

	public int GatherFromHere (int desiredAmount)
	{		
		//if(desiredAmount <= resourcesLeft)
		//{
		//	resourcesLeft -= desiredAmount;
		//	return desiredAmount;
		//}
		//else
		//{
		//	resourcesLeft = 0;			
		//	if(OnEmptySource != null)
		//		OnEmptySource();			
		//	return resourcesLeft;
		//}
        int amount = Mathf.Min(desiredAmount, resourcesLeft);
        resourcesLeft -= desiredAmount;
        if (resourcesLeft <= 0 && OnEmptySource != null)
            OnEmptySource();
        return amount;
	}
}

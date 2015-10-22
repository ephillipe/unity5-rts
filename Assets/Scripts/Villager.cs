using UnityEngine;
using System.Collections;

public class Villager : MobileUnit
{
    public new VillagerProperties properties;

    internal override void Start()
    {
        base.Start();
        properties = new VillagerProperties();
        properties.resourceCapacity = 15;
        properties.unitsGatheredPerSecond = 2;
        properties.unitsBuildedPerSecond = 2;
        properties.movementSpeed = 2;
    }

    private IEnumerator Gather(ResourceRoot source)
    {
        properties.currentResource = source.resource;
        print("called");

        while (!source.IsEmpty)
        {
            yield return StartCoroutine(MoveTo(new Vector3[1] { source.transform.position }));
            while (!properties.IsFull)
            {
                int unitsThatWillBeGathered = Mathf.Min(properties.unitsGatheredPerSecond, (properties.resourceCapacity - properties.currentResourceAmount));
                properties.currentResourceAmount += source.GatherFromHere(unitsThatWillBeGathered);
                yield return new WaitForSeconds(1);
            }

            IResourceReceiver resourceReceiver = UnitController.GetClosestResourceReceiver(properties.currentResource, transform.position);
            if (resourceReceiver != null)
            {
                yield return StartCoroutine(MoveTo(new Vector3[1] { (resourceReceiver as StorageBuilding).transform.position }));
                properties.GiveResources(resourceReceiver);
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    private IEnumerator Build(Construction construction)
    {
        yield return StartCoroutine(MoveTo(new Vector3[1] { construction.transform.position }));
        while (!construction.IsDone)
        {
            construction.Build(properties.unitsBuildedPerSecond);
            yield return new WaitForSeconds(1);
        }
    }

    public override void ActionCallback(ResourceRoot target)
    {
        StopAllCoroutines();
        StartCoroutine(Gather(target));
    }

    public override void ActionCallback(Construction target)
    {
        StopAllCoroutines();
        StartCoroutine(Build(target));
    }
}

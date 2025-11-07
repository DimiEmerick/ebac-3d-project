using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;
    public List<CheckpointBase> checkpoints;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (SaveManager.Instance.Setup != null)
            lastCheckpointKey = SaveManager.Instance.Setup.checkpoint;
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if(i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
        SaveManager.Instance.SaveLastLevel(i);
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}

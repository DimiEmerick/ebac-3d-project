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
        if (SaveManager.Instance != null)
            lastCheckpointKey = SaveManager.Instance.Setup.checkpoint;
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > -1;
    }

    public void SaveCheckpoint(int key, int level)
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        if (checkpoint.key >= 0 ) checkpoint.TurnOff(); 
        if (key > lastCheckpointKey) lastCheckpointKey = key;
        SaveManager.Instance.SaveLastLevel(key, level);
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}

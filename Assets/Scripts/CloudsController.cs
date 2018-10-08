using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class CloudsController : MonoBehaviour
{

	[SerializeField]
	private Cloud[] clouds;

	private TransformAccessArray cloudsTransforms;

	private float maxCloudOffset = 35f;

	private int[] modifiedDirs;

	private NativeArray<int> cloudsDirs, dirsResult;
	private NativeArray<float> cloudsSpeeds;

	private CloudsMovementJob cloudJob;
	private JobHandle cloudJobHandler;
	
	void Start () 
	{
		cloudsDirs = new NativeArray<int>(clouds.Length, Allocator.Persistent);
		dirsResult = new NativeArray<int>(clouds.Length, Allocator.Persistent);
		cloudsSpeeds = new NativeArray<float>(clouds.Length, Allocator.Persistent);
		cloudsTransforms = new TransformAccessArray(clouds.Length);
		
		for (int i = 0; i < clouds.Length; i++)
		{
			cloudsSpeeds[i] = clouds[i].MovementSpeed;
			cloudsTransforms.Add(clouds[i].transform);
		}
		
	}
	
	
	void Update () 
	{
		
		for (int i = 0; i < clouds.Length; i++)
		{
			cloudsDirs[i] = clouds[i].Direction;
		}

		cloudJob = new CloudsMovementJob()
		{
			_cloudsSpeeds = cloudsSpeeds,
			_cloudsDirections = cloudsDirs,
			_maxOffset = maxCloudOffset,
			_resultDirections = dirsResult,
			dt = Time.deltaTime
		};

		cloudJobHandler = cloudJob.Schedule(cloudsTransforms);
		
	}

	private void LateUpdate()
	{
		CompleteCloudsJob();
	}

	private void CompleteCloudsJob()
	{
		cloudJobHandler.Complete();
		
		modifiedDirs = new int[clouds.Length];
		
		dirsResult.CopyTo(modifiedDirs);

		for (int i = 0; i < clouds.Length; i++)
		{
			clouds[i].Direction = modifiedDirs[i];
		}
			
	}

	private void OnDestroy()
	{
		cloudsTransforms.Dispose();
		cloudsSpeeds.Dispose();
		cloudsDirs.Dispose();
		dirsResult.Dispose();
	}
}


struct CloudsMovementJob : IJobParallelForTransform
{
	[ReadOnly]
	public NativeArray<float> _cloudsSpeeds;
	[ReadOnly]
	public NativeArray<int> _cloudsDirections;
	[WriteOnly]
	public NativeArray<int> _resultDirections;
	[WriteOnly]
	public float _maxOffset;

	[WriteOnly] public float dt;
	
	public void Execute(int index, TransformAccess transform)
	{
		int dir = _cloudsDirections[index];
		
		float speed = _cloudsSpeeds[index] * dir;
		
		transform.position += Vector3.right * speed * dt;

		if (transform.position.x > _maxOffset)
		{
			dir = -1;
		}
		else if (transform.position.x < -_maxOffset)
		{
			dir = 1;
		}

		_resultDirections[index] = dir;
	}
}

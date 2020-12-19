using UnityEngine;

public class DamageTile : Tile
{
	GameObject sprite;

	[SerializeField] private int damageAmount;
	void Start()
	{
		sprite = GameObject.CreatePrimitive(PrimitiveType.Quad);
		sprite.renderer.material.mainTexture = (Texture2D)Resources.Load("Textures/Damage", typeof(Texture2D));
		sprite.renderer.material.shader = Shader.Find("Transparent/Cutout/Diffuse");
		sprite.renderer.material.color = new Color(1f, 0f, 0f, 1f);
		sprite.transform.position = new Vector3(transform.position.x,transform.position.y,-.1f);
		damageAmount = Random.Range(10, 20);
	}

	protected override void ApplyEffect(GameObject target)
	{
		var player = target.GetComponent<Player>();
		if (!player) return;
		player.ReceiveDamage(damageAmount);
	}

	void OnDestroy(){
		Destroy(sprite);
	}
}
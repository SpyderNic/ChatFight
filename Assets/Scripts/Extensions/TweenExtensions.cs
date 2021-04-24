using DG.Tweening;

namespace ChatFight
{
	public static class TweenExtensions
	{
		public static void Stop(this Tweener tween, bool complete = false)
		{
			if(tween != null)
			{
				tween.Kill(complete);
			}
		}

		public static void Stop(this Sequence sequence, bool complete = false)
		{
			if(sequence != null)
			{
				sequence.Kill(complete);
			}
		}
	}
}

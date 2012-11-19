namespace Loon.Core.Geom {
	
	public class Curve : Shape {
	
		private Vector2f p1;
	
		private Vector2f c1;
	
		private Vector2f c2;
	
		private Vector2f p2;
	
		private int segments;

        public Curve(Vector2f p1_0, Vector2f c1_1, Vector2f c2_2, Vector2f p2_3)
            : this(p1_0, c1_1, c2_2, p2_3, 20)
        {
			
		}
	
		public Curve(Vector2f p1_0, Vector2f c1_1, Vector2f c2_2, Vector2f p2_3,
				int segments_4) {
			this.p1 = new Vector2f(p1_0);
			this.c1 = new Vector2f(c1_1);
			this.c2 = new Vector2f(c2_2);
			this.p2 = new Vector2f(p2_3);
			this.type = Loon.Core.Geom.ShapeType.CURVE_SHAPE;
			this.segments = segments_4;
			pointsDirty = true;
		}
	
		public Vector2f PointAt(float t) {
			float a = 1 - t;
			float b = t;
	
			float f1 = a * a * a;
			float f2 = 3 * a * a * b;
			float f3 = 3 * a * b * b;
			float f4 = b * b * b;
	
			float nx = (p1.x * f1) + (c1.x * f2) + (c2.x * f3) + (p2.x * f4);
			float ny = (p1.y * f1) + (c1.y * f2) + (c2.y * f3) + (p2.y * f4);
	
			return new Vector2f(nx, ny);
		}
	
		protected internal override void CreatePoints() {
			float step = 1.0f / segments;
			points = new float[(segments + 1) * 2];
			for (int i = 0; i < segments + 1; i++) {
				float t = i * step;
	
				Vector2f p = PointAt(t);
				points[i * 2] = p.x;
				points[(i * 2) + 1] = p.y;
			}
		}
	
		public override Shape Transform(Matrix transform) {
			float[] pts = new float[8];
			float[] dest = new float[8];
			pts[0] = p1.x;
			pts[1] = p1.y;
			pts[2] = c1.x;
			pts[3] = c1.y;
			pts[4] = c2.x;
			pts[5] = c2.y;
			pts[6] = p2.x;
			pts[7] = p2.y;
			transform.Transform(pts, 0, dest, 0, 4);
	
			return new Curve(new Vector2f(dest[0], dest[1]), new Vector2f(dest[2],
					dest[3]), new Vector2f(dest[4], dest[5]), new Vector2f(dest[6],
					dest[7]));
		}
	
		public override bool Closed() {
			return false;
		}
	}
}

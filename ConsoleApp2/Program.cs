// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


/// <summary>
/// Represents a 3D vector with double precision components.
/// </summary>
public struct Vector3f
{
    // Properties for the X, Y, and Z components of the vector
    /// <summary>
    /// Gets the X component of the vector.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Gets the Y component of the vector.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Gets the Z component of the vector.
    /// </summary>
    public double Z { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector3f"/> struct with the specified components.
    /// </summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    /// <param name="z">The Z component of the vector.</param>
    public Vector3f(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Gets the component of the vector at the specified index.
    /// </summary>
    /// <param name="index">The index of the component (0 for X, 1 for Y, 2 for Z).</param>
    /// <returns>The component of the vector at the specified index.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
    public double this[int index]
    {
        get
        {
            double v = index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException(),
            };
            return v;
        }
    }

    /// <summary>
    /// Gets a vector with all components set to zero.
    /// </summary>
    public static Vector3f Zero => new Vector3f(0, 0, 0);

    /// <summary>
    /// Gets a vector representing the unit vector along the X axis.
    /// </summary>
    public static Vector3f OneX => new Vector3f(1, 0, 0);

    /// <summary>
    /// Gets a vector representing the unit vector along the Y axis.
    /// </summary>
    public static Vector3f OneY => new Vector3f(0, 1, 0);

    /// <summary>
    /// Gets a vector representing the unit vector along the Z axis.
    /// </summary>
    public static Vector3f OneZ => new Vector3f(0, 0, 1);

    /// <summary>
    /// Negates the vector, producing a vector with each component multiplied by -1.
    /// </summary>
    /// <param name="a">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector3f operator -(Vector3f a) => new Vector3f(-a.X, -a.Y, -a.Z);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector to subtract from the first.</param>
    /// <returns>The resulting vector after subtraction.</returns>
    public static Vector3f operator -(Vector3f a, Vector3f b) => new Vector3f(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The resulting vector after addition.</returns>
    public static Vector3f operator +(Vector3f a, Vector3f b) => new Vector3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="a">The vector to scale.</param>
    /// <param name="scalar">The scalar value to multiply by.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector3f operator *(Vector3f a, double scalar) => new Vector3f(a.X * scalar, a.Y * scalar, a.Z * scalar);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="a">The vector to scale.</param>
    /// <param name="scalar">The scalar value to divide by.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector3f operator /(Vector3f a, double scalar) => new Vector3f(a.X / scalar, a.Y / scalar, a.Z / scalar);

    /// <summary>
    /// Calculates the dot product of two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public static double Dot(Vector3f a, Vector3f b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    /// <summary>
    /// Calculates the cross product of two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The cross product of the two vectors.</returns>
    public static Vector3f Cross(Vector3f a, Vector3f b) => new Vector3f(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    /// <summary>
    /// Normalizes a vector to have a length of 1.
    /// </summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static Vector3f Unitize(Vector3f vector)
    {
        double length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        return vector / length;
    }

    /// <summary>
    /// Calculates the length (magnitude) of the vector.
    /// </summary>
    /// <returns>The length of the vector.</returns>
    public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);

    /// <summary>
    /// Reflects a vector around a normal vector.
    /// </summary>
    /// <param name="incident">The incident vector to reflect.</param>
    /// <param name="normal">The normal vector to reflect around.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector3f Reflect(Vector3f incident, Vector3f normal)
    {
        return incident - normal * 2 * Dot(incident, normal);
    }

    /*
    /// <summary>
    /// Refracts a vector based on a normal and a refractive index.
    /// </summary>
    /// <param name="incident">The incident vector to refract.</param>
    /// <param name="normal">The normal vector for refraction.</param>
    /// <param name="eta">The refractive index ratio (etai / etat).</param>
    /// <returns>The refracted vector.</returns>
    public static Vector3f Refract(Vector3f incident, Vector3f normal, double eta)
    {
        double cosi = -Math.Max(-1.0, Math.Min(1.0, Dot(incident, normal)));
        double etai = 1, etat = eta;
        Vector3f n = normal;
        if (cosi < 0) { cosi = -cosi; etai = eta; etat = 1; n = -normal; }
        double etaRatio = etai / etat;
        double k = 1 - etaRatio * etaRatio * (1 - cosi * cosi);
        return k < 0 ? Zero : etaRatio * incident + (etaRatio * cosi - Math.Sqrt(k)) * n;
    }
    */

    /// <summary>
    /// Determines if the vector is the zero vector.
    /// </summary>
    /// <returns><c>true</c> if the vector is zero; otherwise, <c>false</c>.</returns>
    internal bool IsZero()
    {
        return X == 0 && Y == 0 && Z == 0;
    }
}

public class Vector3fExample
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // Basic Vector Operations
        Vector3f position = new Vector3f(3, 5, 7);
        Vector3f direction = new Vector3f(1, 0, 0);
        Vector3f velocity = new Vector3f(0, -9.8, 0); // Gravity effect

        // Calculate a new position by moving along a direction
        Vector3f newPosition = position + direction * 5; // Move 5 units along the x-axis

        // Update velocity by adding gravitational effect over time
        Vector3f newVelocity = velocity + new Vector3f(0, -9.8, 0) * 0.1; // Simulate a small time step

        // Dot and Cross Products
        Vector3f vectorA = new Vector3f(1, 0, 0);
        Vector3f vectorB = new Vector3f(0, 1, 0);

        // Calculate the dot product to determine if they are perpendicular
        double dotProduct = Vector3f.Dot(vectorA, vectorB); // Result is 0, vectors are perpendicular

        // Calculate the cross product to find a vector perpendicular to both
        Vector3f perpendicular = Vector3f.Cross(vectorA, vectorB); // Result is (0, 0, 1)

        // Normalization and Length
        Vector3f unnormalizedDirection = new Vector3f(3, 4, 0);
        Vector3f unitDirection = Vector3f.Unitize(unnormalizedDirection); // Result is (0.6, 0.8, 0)

        // Check the length of the original and unit vectors
        double originalLength = unnormalizedDirection.Length(); // Result is 5
        double unitLength = unitDirection.Length(); // Result is 1

        // Reflecting Vectors
        Vector3f incident = new Vector3f(1, -1, 0); // Incoming vector
        Vector3f normal = new Vector3f(0, 1, 0); // Surface normal

        // Calculate the reflection
        Vector3f reflection = Vector3f.Reflect(incident, normal); // Result is (1, 1, 0)

        // Checking for Zero Vectors
        Vector3f motion = new Vector3f(0, 0, 0);
        bool isMotionZero = motion.IsZero(); // Result is true

        Vector3f force = new Vector3f(0, 9.8, 0);
        bool isForceZero = force.IsZero(); // Result is false

        // Using Static Properties for Common Vectors
        Vector3f origin = Vector3f.Zero; // Vector (0, 0, 0)
        Vector3f xAxis = Vector3f.OneX; // Vector (1, 0, 0)
        Vector3f yAxis = Vector3f.OneY; // Vector (0, 1, 0)
        Vector3f zAxis = Vector3f.OneZ; // Vector (0, 0, 1)

        // Define a point relative to these axes
        Vector3f point = origin + xAxis * 2 + yAxis * 3 + zAxis * 4; // Vector (2, 3, 4)

        // Output results to the console for verification
        Console.WriteLine($"New Position: {newPosition}");
        Console.WriteLine($"New Velocity: {newVelocity}");
        Console.WriteLine($"Dot Product: {dotProduct}");
        Console.WriteLine($"Perpendicular Vector: {perpendicular}");
        Console.WriteLine($"Unit Direction: {unitDirection}");
        Console.WriteLine($"Original Length: {originalLength}");
        Console.WriteLine($"Unit Length: {unitLength}");
        Console.WriteLine($"Reflected Vector: {reflection}");
        Console.WriteLine($"Is Motion Zero: {isMotionZero}");
        Console.WriteLine($"Is Force Zero: {isForceZero}");
        Console.WriteLine($"Point: {point}");
    }
}

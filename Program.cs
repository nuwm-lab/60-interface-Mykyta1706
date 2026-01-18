using System;
using System.Text;

namespace LabWork
{
    // 1. ІНТЕРФЕЙС
    public interface IGeometricShape
    {
        void Inita();   // Введення даних
        void Show();    // Виведення даних
        double Size();  // Обчислення об'єму
    }

    // 2. АБСТРАКТНИЙ КЛАС
    public abstract class SolidShape : IGeometricShape
    {
        protected double b1, b2, b3; // Координати центру

        // Конструктор абстрактного класу
        public SolidShape()
        {
            b1 = 0; b2 = 0; b3 = 0;
            Console.WriteLine("-> Викликано конструктор SolidShape (базовий)");
        }

        // Деструктор (Фіналізатор)
        ~SolidShape()
        {
            Console.WriteLine("-> Об'єкт SolidShape видалено з пам'яті");
        }

        // Абстрактні методи, які ОБОВ'ЯЗКОВО мають бути реалізовані в нащадках
        public abstract void Inita();
        public abstract void Show();
        public abstract double Size();
    }

    // 3. КЛАС КУЛЯ (Похідний від абстрактного)
    public class Cball : SolidShape
    {
        public double R;

        public Cball() : base()
        {
            Console.WriteLine("-> Викликано конструктор Cball (Куля)");
        }

        public override void Inita()
        {
            Console.WriteLine("\n--- Налаштування Кулі ---");
            Console.Write("Введiть координати центру (x, y, z) через пробіл: ");
            var parts = Console.ReadLine().Split(' ');
            b1 = double.Parse(parts[0]);
            b2 = double.Parse(parts[1]);
            b3 = double.Parse(parts[2]);
            Console.Write("Введiть радiус R: ");
            R = double.Parse(Console.ReadLine());
        }

        public override void Show()
        {
            Console.WriteLine($"Куля: Центр({b1}, {b2}, {b3}), Радіус={R}");
        }

        public override double Size()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(R, 3);
        }
    }

    // 4. КЛАС ЕЛІПСОЇД (Похідний від абстрактного)
    public class Celipsoid : SolidShape
    {
        public double a1, a2, a3;

        public Celipsoid() : base()
        {
            Console.WriteLine("-> Викликано конструктор Celipsoid (Еліпсоїд)");
        }

        public override void Inita()
        {
            Console.WriteLine("\n--- Налаштування Еліпсоїда ---");
            Console.Write("Введiть координати центру (x, y, z) через пробіл: ");
            var parts = Console.ReadLine().Split(' ');
            b1 = double.Parse(parts[0]);
            b2 = double.Parse(parts[1]);
            b3 = double.Parse(parts[2]);
            Console.Write("Введiть пiв-осi (a1, a2, a3) через пробіл: ");
            var axes = Console.ReadLine().Split(' ');
            a1 = double.Parse(axes[0]);
            a2 = double.Parse(axes[1]);
            a3 = double.Parse(axes[2]);
        }

        public override void Show()
        {
            Console.WriteLine($"Еліпсоїд: Центр({b1}, {b2}, {b3}), Півосі: {a1}, {a2}, {a3}");
        }

        public override double Size()
        {
            return (4.0 / 3.0) * Math.PI * a1 * a2 * a3;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Робота через інтерфейс (Поліморфізм)
            IGeometricShape shape;

            Console.WriteLine("Оберіть фігуру: 1 - Куля, 2 - Еліпсоїд");
            string choice = Console.ReadLine();

            if (choice == "1")
                shape = new Cball();
            else
                shape = new Celipsoid();

            shape.Inita(); // Виклик методу інтерфейсу
            shape.Show();  // Виклик методу інтерфейсу
            Console.WriteLine($"Об'єм: {shape.Size():F2}");

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();

            // Обнуляємо посилання, щоб спрацював деструктор (в теорії GC)
            shape = null;
        }
    }
}
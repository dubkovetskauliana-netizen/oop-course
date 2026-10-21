using System;
using System.Collections.Generic;

namespace ClinicApp.Models;

public class WaitingQueue<T>
{
    private readonly Queue<T> _queue = new Queue<T>();

    // Кількість елементів у черзі
    public int Count => _queue.Count;

    // Чи порожня черга
    public bool IsEmpty => _queue.Count == 0;

    // Додати елемент у кінець черги
    public void Enqueue(T item)
    {
        _queue.Enqueue(item);
    }

    // Забрати першого з черги (з видаленням)
    public T Dequeue()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Немає елементів у черзі. Операція неможлива.");
        }
        return _queue.Dequeue();
    }

    // Подивитися на першого в черзі (без видалення)
    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Черга порожня. Неможливо переглянути елемент.");
        }
        return _queue.Peek();
    }

    // Перетворення поточного стану черги в масив для виводу
    public T[] ToArray()
    {
        return _queue.ToArray();
    }
}

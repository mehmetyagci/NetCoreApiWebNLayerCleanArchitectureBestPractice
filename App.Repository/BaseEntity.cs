﻿namespace App.Repository;

public abstract class BaseEntity<T>
{
    public T Id { get; set; } = default!;
}
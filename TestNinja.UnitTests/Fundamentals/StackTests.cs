using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class StackTests
{
    private CustomStack<string> stack;

    [SetUp]
    public void SetUp()
    {
        stack = new CustomStack<string>();
    }

    [Test]
    public void Push_WhenNull_ThrowArgumentNullException()
    {
        Assert.That(() => stack.Push(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Push_WhenValidObject_AddValueTOStack()
    {
        // Act
        stack.Push("new item");

        // Assert 
        Assert.That(stack.Peek(), Is.EqualTo("new item"));
    }

    [Test]
    public void Pop_WhenEmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => stack.Pop(), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Pop_WhenCalled_ReturnLastItemFromStack()
    {
        // Arrange
        stack.Push("My test item1");
        stack.Push("My test item2");
        stack.Push("My test item3");

        // Act
        var result = stack.Pop();

        // Assert 
        Assert.That(result, Is.EqualTo("My test item3"));
    }

    [Test]
    public void Pop_WhenCalled_RemoveLastItemFromStack()
    {
        // Arrange
        stack.Push("My test item1");
        stack.Push("My test item2");
        stack.Push("My test item3");

        // Act
        stack.Pop();

        // Assert 
        Assert.That(stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Peek_WhenEmptyStack_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => stack.Peek());
    }

    [Test]
    public void Peek_WhenCalled_ReturnTheLastItemOfStack()
    {
        // Arrange
        stack.Push("My test item");

        // Act
        var result = stack.Peek();

        // Assert 
        Assert.That(result, Is.EqualTo("My test item"));
    }

    [Test]
    public void Peek_StackWithObjects_DoesntRemoveObjectOnTopOfTheStack()
    {
        // Arrange
        stack.Push("a");

        // Act
        stack.Peek();

        // Assert 
        Assert.That(stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Count_WhenItemAdded_ChangesItsValue()
    {
        // Act
        stack.Push("a");

        // Assert 
        Assert.That(stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Count_EMptyStack_ReturnZero()
    {
        Assert.That(stack.Count, Is.Zero);
    }
}
using System;
using FluentAssertions;
using TareasAPI.Validation;
using Xunit;

namespace TareasAPI.Tests
{
 public class DateValidatorTests
 {
 [Fact]
 public void IsStartBeforeEnd_WhenStartIsBeforeEnd_ShouldReturnTrue()
 {
 // Arrange
 var start = new DateTime(2024,1,1);
 var end = new DateTime(2024,12,31);

 // Act
 var result = DateValidator.IsStartBeforeEnd(start, end);

 // Assert
 result.Should().BeTrue();
 }

 [Fact]
 public void IsStartBeforeEnd_WhenStartIsAfterEnd_ShouldReturnFalse()
 {
 // Arrange
 var start = new DateTime(2024,12,31);
 var end = new DateTime(2024,1,1);

 // Act
 var result = DateValidator.IsStartBeforeEnd(start, end);

 // Assert
 result.Should().BeFalse();
 }

 [Fact]
 public void IsStartBeforeEnd_WhenStartIsEqualToEnd_ShouldReturnFalse()
 {
 // Arrange
 var start = new DateTime(2024,6,15);
 var end = new DateTime(2024,6,15);

 // Act
 var result = DateValidator.IsStartBeforeEnd(start, end);

 // Assert
 result.Should().BeFalse();
 }

 [Fact]
 public void IsStartBeforeEnd_WhenStartIsNull_ShouldThrowArgumentNullException()
 {
 // Arrange
 DateTime? start = null;
 var end = new DateTime(2024,12,31);

 // Act
 Action act = () => DateValidator.IsStartBeforeEnd(start, end);

 // Assert
 act.Should().Throw<ArgumentNullException>()
 .WithParameterName("start");
 }

 [Fact]
 public void IsStartBeforeEnd_WhenEndIsNull_ShouldThrowArgumentNullException()
 {
 // Arrange
 var start = new DateTime(2024,1,1);
 DateTime? end = null;

 // Act
 Action act = () => DateValidator.IsStartBeforeEnd(start, end);

 // Assert
 act.Should().Throw<ArgumentNullException>()
 .WithParameterName("end");
 }

 [Theory]
 [InlineData("2024-01-01", "2024-01-02", true)]
 [InlineData("2024-01-02", "2024-01-01", false)]
 [InlineData("2024-06-15", "2024-06-15", false)]
 public void IsStartBeforeEnd_WithVariousDates_ShouldReturnExpectedResult(
 string startDate, string endDate, bool expected)
 {
 // Arrange
 var start = DateTime.Parse(startDate);
 var end = DateTime.Parse(endDate);

 // Act
 var result = DateValidator.IsStartBeforeEnd(start, end);

 // Assert
 result.Should().Be(expected);
 }
 }
}

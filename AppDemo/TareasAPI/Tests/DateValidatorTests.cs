using System;
using Xunit;
using TareasAPI;

namespace TareasApi.Tests
{
 public class DateValidatorTests
 {
 [Fact]
 public void BothNull_ThrowsArgumentNullException()
 {
 Assert.Throws<ArgumentNullException>(() => DateValidator.IsStartBeforeEnd(null, null));
 }

 [Fact]
 public void StartNull_ThrowsArgumentNullException()
 {
 Assert.Throws<ArgumentNullException>(() => DateValidator.IsStartBeforeEnd(null, DateTime.Now));
 }

 [Fact]
 public void EndNull_ThrowsArgumentNullException()
 {
 Assert.Throws<ArgumentNullException>(() => DateValidator.IsStartBeforeEnd(DateTime.Now, null));
 }

 [Fact]
 public void StartBeforeEnd_ReturnsTrue()
 {
 var start = new DateTime(2025,1,1);
 var end = new DateTime(2025,1,2);
 Assert.True(DateValidator.IsStartBeforeEnd(start, end));
 }

 [Fact]
 public void StartEqualEnd_ReturnsFalse()
 {
 var dt = new DateTime(2025,1,1);
 Assert.False(DateValidator.IsStartBeforeEnd(dt, dt));
 }

 [Fact]
 public void StartAfterEnd_ReturnsFalse()
 {
 var start = new DateTime(2025,1,3);
 var end = new DateTime(2025,1,2);
 Assert.False(DateValidator.IsStartBeforeEnd(start, end));
 }
 }
}

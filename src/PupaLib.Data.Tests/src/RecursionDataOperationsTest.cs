using PupaLib.Data.Tests.Data;

using Xunit.Abstractions;

namespace PupaLib.Data.Tests;

[CollectionDefinition("RecursionData test", DisableParallelization = false)]
public sealed class RecursionDataOperationsTest(ITestOutputHelper testOutputHelper) {
   private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

   [Fact(DisplayName = "Create -> Set -> Get")]
   public void RecursionCreateSetGet() {
      var gameData = new GameData();
      gameData.Set("isFirstTime", true);
      gameData.DeepSet("player.jumpPower", 5f);
      gameData.DeepSet("player.speed", 5f);
      gameData.DeepSet("player.maxHealth", 10f);
      gameData.DeepSet("player.health", 6.5f);
      gameData.DeepSet("player.cash", 100);
      Assert.True(gameData.Get<bool>("isFirstTime"));
      Assert.Equal(5f, gameData.DeepGet<float>("player.jumpPower"));
      Assert.Equal(5f, gameData.DeepGet<float>("player.speed"));
      Assert.Equal(10f, gameData.DeepGet<float>("player.maxHealth"));
      Assert.Equal(6.5f, gameData.DeepGet<float>("player.health"));
      Assert.Equal(100, gameData.DeepGet<int>("player.cash"));
   }
   [Fact(DisplayName = "Create -> Set -> Exists")]
   public void RecursionCreateSetExists() {
      var gameData = new GameData();
      gameData.Set("isFirstTime", true);
      gameData.DeepSet("player.jumpPower", 5f);
      gameData.DeepSet("player.speed", 5f);
      gameData.DeepSet("player.maxHealth", 10f);
      gameData.DeepSet("player.health", 6.5f);
      gameData.DeepSet("player.cash", 100);
      Assert.True(gameData.ExistsWithType<bool>("isFirstTime"));
      Assert.True(gameData.DeepExistsWithType<float>("player.jumpPower"));
      Assert.True(gameData.DeepExistsWithType<float>("player.speed"));
      Assert.True(gameData.DeepExistsWithType<float>("player.maxHealth"));
      Assert.True(gameData.DeepExistsWithType<float>("player.health"));
      Assert.True(gameData.DeepExistsWithType<int>("player.cash"));
   }
}
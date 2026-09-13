using System.Collections.Concurrent;
using System.Text;
using System.Text.Json.Serialization;

namespace PupaLib.Data;

[Serializable]
public class RecursionData {
   public const char SeparateSymbol = '.';

   public RecursionData() {
      Data = new ConcurrentDictionary<string, object>();
   }

   [JsonPropertyName("data")] 
   public ConcurrentDictionary<string, object> Data { get; private set; }

   [JsonPropertyName("childs")] 
   public ConcurrentDictionary<string, RecursionData> Childs { get; private set; } = null!;

   public bool IsParent => Childs != null && Childs.Any();

   #region String

   public override string ToString() {
      var sb = new StringBuilder();
      DeepString(sb, 0);
      return sb.ToString();
   }

   private void DeepString(StringBuilder sb, int index) {
      var tabs = new string('\t', index);

      foreach (var data in Data) 
         sb.Append($"{tabs}[{data.Key}] -> {data.Value}\n");

      if (IsParent)
         foreach (var childPair in Childs) {
            sb.Append($"{tabs}\t/{childPair.Key}\n");
            childPair.Value.DeepString(sb, index + 1);
         }
   }

   #endregion

   #region SET

   public void DeepSet<T>(string path, T content) where T : notnull {
      RecursionSet<T>(path.Split(SeparateSymbol), content, 0);
   }

   private void RecursionSet<T>(string[] names, T content, int index) where T : notnull {
      var name = names[index];
      if (index < names.Length - 1) {
         Childs ??= new ConcurrentDictionary<string, RecursionData>();
         Childs.TryAdd(name, new RecursionData());
         Childs[name].RecursionSet<T>(names, content, index + 1);
         return;
      }

      Data[name] = content;
   }
   
   public void Set<T>(string id, T content) where T : notnull {
      Data[id] = content;
   }

   #endregion

   #region GET

   public T Get<T>(string id) {
      return (T)Convert.ChangeType(Data[id], typeof(T));
   }

   public T DeepGet<T>(string path) {
      return RecursionGet<T>(path.Split(SeparateSymbol), 0);
   }

   private T RecursionGet<T>(string[] names, int index) {
      var name = names[index];
      if (names.Length - 1 != index) return Childs[name].RecursionGet<T>(names, index + 1);

      return Get<T>(name);
   }

   #endregion
}
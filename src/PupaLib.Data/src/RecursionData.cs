using System.Collections.Concurrent;
using System.Text;
using System.Text.Json.Serialization;

namespace PupaLib.Data;

[Serializable]
public class RecursionData {
   public const char SeparateSymbol = '.';

   [JsonPropertyName("data")] 
   public ConcurrentDictionary<string, object> Data { get; private set; } = new();

   [JsonPropertyName("childs")] 
   public ConcurrentDictionary<string, RecursionData> Childs { get; private set; } = null!;

   [JsonIgnore]
   public bool IsParent => Childs != null && Childs.Any();

   #region String

   public override string ToString() {
      var sb = new StringBuilder();
      RecursionString(sb, 0);
      return sb.ToString();
   }

   private void RecursionString(StringBuilder sb, int index) {
      var tabs = new string('\t', index);

      foreach (var data in Data) 
         sb.Append($"{tabs}[{data.Key}] -> {data.Value}\n");

      if (IsParent)
         foreach (var childPair in Childs) {
            sb.Append($"{tabs}\t/{childPair.Key}\n");
            childPair.Value.RecursionString(sb, index + 1);
         }
   }

   #endregion

   #region Exists
   
   public bool ExistsWithType<T>(string name) where T : notnull {
      var result = Data.TryGetValue(name, out var value);
      return result && value!.GetType() == typeof(T);
   }
   
   public bool DeepExistsWithType<T>(string path) where T : notnull {
      return RecursionExistsWithType<T>(path.Split(SeparateSymbol), 0);
   }

   private bool RecursionExistsWithType<T>(string[] names, int index) where T : notnull {
      var name = names[index];
      if (index < names.Length - 1) {
         return Childs[name].RecursionExistsWithType<T>(names, index + 1);
      }

      return ExistsWithType<T>(name);
   }
   
   public bool ExistsAny(string name) {
      return Data.TryGetValue(name, out _);
   }
   
   public bool DeepExistsAny(string path) {
      return RecursionExistsAny(path.Split(SeparateSymbol), 0);
   }

   private bool RecursionExistsAny(string[] names, int index) {
      var name = names[index];
      if (index < names.Length - 1) {
         return Childs[name].RecursionExistsAny(names, index + 1);
      }

      return ExistsAny(name);
   }


   #endregion
   
   #region Set
   
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
   
   public void Set<T>(string name, T content) where T : notnull {
      Data[name] = content;
   }

   #endregion

   #region Get

   #region Cast

   

   public T GetCast<T>(string name) {
      return (T)Data[name];
   }
   
   public T DeepGetCast<T>(string path) {
      return RecursionGetCast<T>(path.Split(SeparateSymbol), 0);
   }
   
   private T RecursionGetCast<T>(string[] names, int index) {
      var name = names[index];
      if (names.Length - 1 != index) 
         return Childs[name].RecursionGetCast<T>(names, index + 1);

      return GetCast<T>(name);
   }
   #endregion

   #region Obj
   public object GetObj(string name) {
      return Data[name];
   }

   public object DeepGetObj(string path) {
      return RecursionGetObj(path.Split(SeparateSymbol), 0);
   }
   
   private object RecursionGetObj(string[] names, int index) {
      var name = names[index];
      if (names.Length - 1 != index) 
         return Childs[name].RecursionGetObj(names, index + 1);

      return GetObj(name);
   }
   #endregion


   #region Convert
   public T Get<T>(string name) {
      return (T)Convert.ChangeType(Data[name], typeof(T));
   }

   public T DeepGet<T>(string path) {
      return RecursionGet<T>(path.Split(SeparateSymbol), 0);
   }

   private T RecursionGet<T>(string[] names, int index) {
      var name = names[index];
      if (names.Length - 1 != index) 
         return Childs[name].RecursionGet<T>(names, index + 1);

      return Get<T>(name);
   }
   #endregion
   

   #endregion
}
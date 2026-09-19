<div align="center">

# 🗃️ PupaLib.Data

![PupaLib.Data](https://img.shields.io/badge/PupaLib.Data-black?style=for-the-badge&logo=PupaLib.Data&logoColor=white)
![License](https://img.shields.io/badge/MIT-black?style=for-the-badge)
![Dotnet](https://img.shields.io/badge/.NET-black?style=for-the-badge&logo=dotnet&logoColor=white)
![Nuget](https://img.shields.io/badge/NuGet-black?style=for-the-badge&logo=nuget&logoColor=white)
![Github]( https://img.shields.io/badge/GitHub-black?style=for-the-badge&logo=github&logoColor=white)
![License](https://img.shields.io/badge/MIT-black?style=for-the-badge)
![C#](https://img.shields.io/badge/C%23-black.svg?style=for-the-badge&logo=csharp&logoColor=white)


![NuGet](https://img.shields.io/nuget/v/PupaLib.FileIO.svg?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-10.0-blue?style=for-the-badge)

<!-- ![.Version](https://img.shields.io/github/v/release/Artpupser/PupaLib.Data?style=for-the-badge) -->
<!-- ![.NET](https://img.shields.io/badge/.NET-10.0-blue?style=for-the-badge) -->
<!-- ![NuGet](https://img.shields.io/nuget/v/PupaLib.Data.svg?style=for-the-badge) -->

#### [PupaLib.Data](https://github.com/Artpupser/PupaLib.Data) is a lightweight recursive key-value storage with path-based access. 🎯

<img src="https://github.com/Artpupser/PupaLib.Data/blob/main/assets/banner.jpg" style="border-radius: 20px; max-height: 500px">

</div>

---
## 📎 Navigation

- [✨ Features](#-features)
- [🧵 Usage](#-usage)
- [🚀 Installation](#-installation)
- [📦 Dependencies](#-dependencies)
- [🗃️ Devlog](#devlog)
- [⚖️️ License](#-license)


## ✨️ Features

<div align="center">

| 🏆 Feature              | 📝 Description                                             |
| ----------------------- | ---------------------------------------------------------- |
| **Recursive Structure** | Tree-like data storage with nested nodes (`Childs`)        |
| **Path-based Access**   | Access data via dot-separated paths (`player.stats.hp`)    |
| **Thread-safe**         | Built on `ConcurrentDictionary`                            |
| **Deep Operations**     | `DeepSet`, `DeepGet`, `DeepExists*` for nested access      |
| **Auto Node Creation**  | Nodes are created automatically during deep set operations |
| **Type Safety Checks**  | Validate types with `ExistsWithType<T>`                    |
| **Debug Visualization** | Built-in `ToString()` for readable tree output             |
</div>

## 🚀 Installation

You can install the package through the NuGet Package Manager or via the command line

```bash
dotnet add package PupaLib.Data
```

Or through the NuGet Package Manager

```bash
Install-Package PupaLib.Data
```

## 🧵 Usage

### 📌 Create instance

```csharp
var data = new RecursionData();
```

---

### 📌 Set values

```csharp
// Simple set
data.Set("score", 100);

// Deep set (auto-creates structure)
data.DeepSet("player.stats.hp", 250);
data.DeepSet("player.stats.mana", 75);
```

---

### 📌 Get values

```csharp
int score = data.Get<int>("score");

int hp = data.DeepGet<int>("player.stats.hp");
```

---

### 📌 Check existence

```csharp
bool exists = data.ExistsAny("score");

bool deepExists = data.DeepExistsAny("player.stats.hp");

bool typed = data.ExistsWithType<int>("score");

bool deepTyped = data.DeepExistsWithType<int>("player.stats.hp");
```

---

### 📌 Debug output

```csharp
Console.WriteLine(data);
```

Example output:

```
[score] -> 100
    /player
        /stats
            [hp] -> 250
            [mana] -> 75
```

---

### 📌 Path separator

```csharp
RecursionData.SeparateSymbol = '.';
```

Default separator is `.`

---

## 📦 Dependencies

None

## 🗃️ Devlog

### 0.1.0

* Initial implementation of `RecursionData`
* Path-based access using `.` separator
* Thread-safe storage via `ConcurrentDictionary`
* Core API: `Set`, `Get`, `Exists`, `Deep*`

## ⚖️ License

This project is licensed under the **MIT License**.

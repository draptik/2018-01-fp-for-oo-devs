# Funktionale Programmierung
## für OO Entwickler

Patrick Drechsler

<span class="small">Softwerkskammer Nürnberg 1.2.2018</small>

<<= x =>>

## Was ist Funktionale Programmierung?

- Sprachunabhängig
- **Nur ein Paradigma!**
    - andere Paradigmen:
         - Prozedural
         - Objektorientiert
         - Logisch

<<= x =>>

<section tagcloud large>
    <span tagcloud-weight="16">Unit </span>
    <span tagcloud-weight="44">Currying </span>
    <span tagcloud-weight="29">Higher Order Functions </span>
    <span tagcloud-weight="10">Event Sourcing/CQRS </span>
    <span tagcloud-weight="35">Applicatives </span>
    <span tagcloud-weight="13">Monad </span>
    <span tagcloud-weight="30">filter/map/reduce </span>
    <span tagcloud-weight="18">bind </span>
    <span tagcloud-weight="40">side effects </span>
    <span tagcloud-weight="22">purity </span>
    <span tagcloud-weight="39">honest functions </span>
    <span tagcloud-weight="19">Functor </span>
    <span tagcloud-weight="50">Immutability </span>
    <span tagcloud-weight="34">category theory </span>
    <span tagcloud-weight="15">Monoid </span>
    <span tagcloud-weight="29">tuples  </span> 
    <span tagcloud-weight="17">discriminated unions </span>
    <span tagcloud-weight="20">elevated types </span>
    <span tagcloud-weight="33">Typed FP </span>
    <span tagcloud-weight="28">Either </span>
    <span tagcloud-weight="34">Option </span>
    <span tagcloud-weight="14">arrow notation </span>
    <span tagcloud-weight="24">railway oriented programming </span>
    <span tagcloud-weight="26">Lambda </span>
    <span tagcloud-weight="12">Composition </span>
<section>


<<= x =>>

## FP Konzepte

<<= x =>>

### Immutability
- Lambdas: Sprachfeatures verwenden (LINQ, Streaming API)
- Value Objects ("fight primitive obsession")

Immutability ist eine häufige Voraussetzung für viele weiteren FP Konzepte.

<-- v -->

das ist ok:
```javascript
let list = [1, 2, 3, 4, 5];
for (let i = 0; i < list.length; i++) {
    list[i] = list[i] + 1;
}
console.log(list)
```

...aber das einfacher:
```javascript
let list = [1, 2, 3, 4, 5];
let result = list.map(x -> x + 1); // oder eine "addOne" Funktion nehmen
console.log(list)
```

<-- v -->

ok...
```csharp
public Risk CheckRisk(int age) // <- primitive obsession
{
    if (age <= 0) { /* error handling */ }
    else if (age > 120) { /* error handling */ }
    else if (age < 20) { return Risk.Low }
    else if (age < 40) { return Risk.Medium }
    else { return Risk.High }
}
```

...weniger "Krach":
```csharp
// "Age": immutable value object
public Risk CheckRisk(Age age)
{
    if (age < 20) { return Risk.Low }
    else if (age < 40) { return Risk.Medium }
    else { return Risk.High }
}
```

<<= x =>>

### Mehr Rechte für Funktionen! 
- **Expressions** statt Statements
- **Higher Order Functions**: Methoden können auch Funktionen zurückgeben
    - &#10137; Currying/Applicative Functions

<-- v -->

#### Expressions

```csharp
// statement
public int AddOne(int i)
{
    return i + 1;
}
```

```csharp
// expression
public int AddOne(int i) => i + 1;
```

<-- v -->

#### Higher Order Functions

```csharp
// int -> (int -> bool)
Func<int, bool> IsDivisibleBy(int divisor) => num => num % divisor == 0;
// (int -> bool)
var isDivisibleByFive = IsDivisibleBy(5);

isDivisibleByFive(10); // TRUE
```

```fsharp
// int -> (int -> bool)
let isDivisibleBy divisor = (fun num -> num % divisor = 0)
// (int -> bool)
let isDivisibleByFive = isDivisibleBy 5

10 |> isDivisibleByFive // TRUE
```

<<= x =>>

### Composition
- Funktionen miteinander kombinieren (Alternative zu Ableitung in OO)
    - z.B. Method Chaining (LINQ)
    - &#10137; kann IoC ersetzen

<-- v -->

#### Composition

<pre>
<code data-noescape data-trim class="lang-csharp hljs">
Func&lt;int, bool> isLargerThanFive = x => x > 5;
Func&lt;int, bool> isSmallerThenTen = x => x < 10;

<span class="mycodemark-always">Func&lt;int, bool> isBetweenFiveAndTen = x => 
    isLargerThanFive(x) && isSmallerThenTen(x);</span>

isBetweenFiveAndTen(7); // TRUE
</code>
</pre>

<-- v -->

#### Composition

<pre>
<code data-noescape data-trim class="lang-csharp hljs">
static string Abbreviate(string s) => s.SubString(0, 2).ToLower();

static string AbbreviateName(Person p) 
    => Abbreviate(p.FirstName) + Abbreviate(p.LastName);

static string AppendDomain(string localPart) 
    => $"{localPart}@company.com";

// composition
<span class="mycodemark-always">Func&lt;Person, string> emailFor = p => AppendDomain(AbbreviateName(p));</span>

var joe = new Person("Joe", "Smith")
<span class="mycodemark-always">emailFor(joe).Should().Be("josm@company.com");</span>
</code>
</pre>

<pre>
<code data-noescape data-trim class="lang-csharp hljs">
// method chaining (using C# Extensions)
static string AbbreviateName(<span class="mycodemark-always">this</span> Person p) 
    => Abbreviate(p.FirstName) + Abbreviate(p.LastName);
    
static string AppendDomain(<span class="mycodemark-always">this</span> string localPart) 
    => $"{localPart}@company.com";

<span class="mycodemark-always">joe.AbbreviateName().AppendDomain().Should().Be("josm@company.com");</span>
</code>
</pre>

<-- v -->

#### Composition

<pre>
<code data-noescape data-trim class="lang-fsharp hljs">
let add1 x = x + 1
let times2 x = x * 2

let add1Times2 x = times2(add1 x) // ok...
<span class="mycodemark-always">
let add1Times2 = add1 >> times2   // ">>": composition operator
</span>
</code>
</pre>

<pre>
<code data-noescape data-trim class="lang-fsharp hljs">
open System
type Person = { FirstName: string; LastName: string }
let p = {FirstName = "Joe"; LastName = "Smith"}
let abbreviate (s: string) = s.[0..1].ToLower()
let abbreviateName p = abbreviate(p.FirstName) + abbreviate(p.LastName)
let appendDomain (s: string) = s + "@company.com"
<span class="mycodemark-always">
let emailFor = abbreviateName >> appendDomain
</span>
p |> emailFor // josm@company.com
</code>
</pre>

<<= x =>>

### Safety through Types
- Stärkeres Typsystem kann Entwicklung erleichtern
    - Discriminated Union
    - Wrapper wie Option, Either, etc

<-- v -->

#### Typsystem

<pre>
<code data-noescape data-trim class="lang-csharp hljs">
public <span class="mycodemark-always">Option&lt;Customer></span> GetCustomer(int id) { /* ... */ }

public string Greet(int id) 
    => GetCustomer(id)<span class="mycodemark-always">.Match(
            None:</span> () => "Sorry, who?",
            <span class="mycodemark-always">Some:</span> (customer) => $"Hello, {customer.Name}");
</code>
</pre>

<-- v -->

#### Typsystem mit Business-Logik

```fsharp
open System
type AccountStatus = // discriminated union
    Requested | Active | Frozen | Dormant | Closed

type CurrencyCode = string // "type alias"

type Transaction = { // record type
    Amount: decimal
    Description: string
    Date: DateTime
}    

type AccountState = {
    Status: AccountStatus
    Currency: CurrencyCode
    AllowedOverdraft: decimal
    TransactionHistory: Transaction list
}

type AccountState with
member this.WithStatus(status) = { this with Status = status }
member this.Add(transaction) = 
    { this with TransactionHistory = 
        transaction :: this.TransactionHistory }
```    

<<= x =>>

## Zusammenfassung

- Immutability
- Expressions
- HOF
- Composition
- Typsystem

<<= x =>>

Vorschläge?
<!-- .slide: class="too-much-content" -->
- welche FP Konzepte sind für OO Programmierer interessant?
- **in welcher Reihenfolge sollte diese Konzepte vorgestellt werden?**
- Konzepte: immutability, lambdas (filter/map/reduce), applicatives, HOF, option, typed FP
- **was wird immer falsch gemacht bei der Einführung in FP?**
- was sind die einfachen, was die schwierigen Konzepte von FP?
- **welche Konzepte beißen sich (OO vs FP)?**
- **Erfahrungen aus der Praxis**
- Unterschiede beim Testing (FP Leute machen gerne REPL plus Property Based Testing)

<<= x =>>

# Danke!

Kontaktinfos:

- <i class="fa fa-twitter" aria-hidden="true"></i>&nbsp;@drechsler
- <i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;socialcoding@pdrechsler.de


<<= x =>>

### Resources
<!-- .slide: class="too-much-content" -->

- Videos
    - [One kata, 3 languages](https://www.youtube.com/watch?v=Ux5wUSOsEfc&t=1106s)
    - [Functional Principles for Object-Oriented Development](https://www.youtube.com/watch?v=GpXsQ-NIKXY)
    - [What Every Hipster Should Know About Functional Programming](https://vimeo.com/68331937)
    - [Don't fear the Monad](https://www.youtube.com/watch?v=ZhuHCtR3xq8)
- Blog
    - [Less is more: language features](http://blog.ploeh.dk/2015/04/13/less-is-more-language-features/)
    - [Partial Application in C#](http://mikehadlow.blogspot.de/2015/09/partial-application-in-c.html)
- Books
    - Functional Programming in C#. *Enrico Buonanno*
    - Domain modeling made Functional. *Scott Wlaschin*
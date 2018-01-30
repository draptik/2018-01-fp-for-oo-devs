## FP for OO developers

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
public Risk CheckRisk(int age)
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
public Risk CheckRisk(Age age)
{
    if (age < 20) { return Risk.Low }
    else if (age < 40) { return Risk.Medium }
    else { return Risk.High }
}
```

<<= x =>>

### Mehr Rechte für Funktionen! 
- Expressions statt Statements
- Higher Order Functions: Methoden können auch Funktionen zurückgeben
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
Func<int, bool> IsDivisibleBy(int divisor) => number 
    => number % divisor == 0;

var isDivisibleByFive = IsDivisibleBy(5);
isDivisibleByFive(10); // TRUE
```

<<= x =>>

### Composition
- Funktionen miteinander kombinieren (Alternative zu Ableitung in OO)
    - z.B. Method Chaining (LINQ)
    - &#10137; kann IoC ersetzen

<-- v -->

#### Composition

```csharp
Func<int, bool> isLargerThanFive = x => x > 5;
Func<int, bool> isSmallerThenTen = x => x < 10;

Func<int, bool> isBetweenFiveAndTen = x => 
    isLargerThanFive(x) && isSmallerThenTen(x);

isBetweenFiveAndTen(7).Should().BeTrue();
```

<-- v -->

#### Composition

```csharp
static string Abbreviate(string s) => s.SubString(0, 2).ToLower();

static string AbbreviateName(Person p) 
    => Abbreviate(p.FirstName) + Abbreviate(p.LastName);

static string AppendDomain(string localPart) 
    => $"{localPart}@company.com";

// composition
Func<Person, string> emailFor = p => AppendDomain(AbbreviateName(p));

var joe = new Person("Joe", "Smith")
emailFor(joe).Should().Be("josm@company.com");
```

```csharp
// method chaining (using C# Extensions)
static string AbbreviateName(this Person p) 
    => Abbreviate(p.FirstName) + Abbreviate(p.LastName);
    
static string AppendDomain(this string localPart) 
    => $"{localPart}@company.com";

joe.AbbreviateName().AppendDomain().Should().Be("josm@company.com");
```

<<= x =>>

### Safety through Types
- Stärkeres Typsystem kann Entwicklung erleichtern
    - Discriminated Union
    - Wrapper wie Option, Either, etc

<-- v -->

#### Typsystem

```csharp
public Option<Customer> GetCustomer(int id) { /* ... */ }

public string Greet(int id) 
    => GetCustomer(id).Match(
            None: () => "Sorry, who?",
            Some: (customer) => $"Hello, {customer.Name}");
```    

<-- v -->

#### Typsystem (Bsp. F#)

```fsharp
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
member this.WithStatus(status) = { this with Status = Active }
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

# Danke!

Kontaktinfos:

- <i class="fa fa-twitter" aria-hidden="true"></i>&nbsp;@drechsler
- <i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;socialcoding@pdrechsler.de
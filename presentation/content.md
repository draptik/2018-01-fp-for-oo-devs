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

<-- v -->

todo

<!-- ![noborder-fixed](resources/transitive-dependency-1.png) -->

<<= x =>>

<section tagcloud shuffle small>
    Immutability
    Higher Order Functions
    Monad
    Functor
    Monoid
    Applicatives
    Currying
    filter/map/reduce
    bind
    side effects
    purity
    honest functions
    tuples
    discriminated unions
    elevated types
    Typed FP
    Either
    Option
    Unit
    arrow notation
    Lambda
    Composition
    Event Sourcing/CQRS
<section>


<<= x =>>


<!-- 
<section tagcloud shuffle>
    <span tagcloud-weight="55">Immutability</span>
    <span tagcloud-weight="40">Higher Order Functions λ</span>
    <span tagcloud-weight="10">Monade</span> <span tagcloud-weight="10">Functor</span> <span tagcloud-weight="10">Monoid</span>
    <span tagcloud-weight="30">Currying</span> <span tagcloud-weight="30">Applicatives</span>
    <span tagcloud-weight="5">Currying</span>
    <span tagcloud-weight="5">Currying</span>
<section>     -->

<<= x =>>

## FP Konzepte

<<= x =>>

### Immutability
- Lambdas: Sprachfeatures verwenden (LINQ, Streaming API)
- Value Objects ("fight primitive obsession")

<<= x =>>

### Mehr Rechte für Funktionen! 
- Expressions statt Statements
- Higher Order Functions: Methoden können auch Funktionen zurückgeben
    - &#10137; Currying/Applicative Functions

<<= x =>>

### Composition
- Funktionen miteinander kombinieren (Alternative zu Ableitung in OO)
    - &#10137; kann IoC ersetzen

<<= x =>>

### Safety through Types
- Stärkeres Typsystem kann Entwicklung erleichtern
    - DU
    - Wrapper wie Option, Either, etc
    
    


<<= x =>>

## Zusammenfassung

<<= x =>>

## Links

<<= x =>>

# Fragen?

Kontaktinfos:

- <i class="fa fa-twitter" aria-hidden="true"></i>&nbsp;@drechsler
- <i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;socialcoding@pdrechsler.de
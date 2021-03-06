## Krok 2. Równość ułamków

Krok ten będzie miał niewiele zadań do wykonania (literalnie), ale jest prawdopodobnie najtrudniejszy.

Określisz w nim pojęcie "takie same", czyli kiedy dwa ułamki są sobie równe (implementacja `IEquatable`, `IEquatable<Ulamek>`, generowanie _hashkodu_, przeciążenie operatorów `==` oraz `!=`), utworzysz testy jednostkowe.

Wykonuj zadania w podanej kolejności.

### Zadania do wykonania

1. W projekcie typu _Class library_ dodaj nową klasę. Plik nazwij `UlamekEquals.cs`.

2. Zmień nazwę klasy na `Ulamek`. Dodaj słowo kluczowe `partial` przed `class`. Korzystasz z funkcjonalności dzielenia klasy na wiele plików [`partial class`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods). Rozbudowę klasy `Ulamek` w zakresie tego kroku przeprowadzisz w tym pliku.

3. W projekcie z testami jednostkowymi utwórz plik o nazwie `UnitTestUlamekEquals.cs`. Możesz to wykonać kolejno poleceniami: *Add > New Item .. > Basic Unit Test* (w tej sytuacji wszystkie wymagane adnotacje wstawione zostaną do kodu klasy testującej). Testy jednostkowe związane z implementacją równości ułamków zapisz w tym pliku.

4. Opracuj odpowiednie przesłonięcie metody `Equals()`. Równocześnie **MUSISZ** przeciążyć `GetHashCode()`.

5. Zaimplementuj interfejs `IEquatable<Ulamek>`.

6. Zaimplementuj statyczną wersję metody `Equals` o sygnaturze:

    ```csharp
    public static bool Equals(Ulamek u1, Ulamek u2)
    ```

7. Zaimplementuj przeciążenie operatora `==`. Będziesz **MUSIAŁ** równocześnie zaimplementować przeciążenie operatora `!=`.

8. Wszystkie powyższe metody muszą być wzajemnie spójne!

9. Opracuj testy jednostkowe

### Podpowiedzi

1. Poczytaj o przeciążaniu `Equals` w C#:
  
   * <https://github.com/loganfranken/overriding-equals-in-c-sharp>

   * <https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/equality-operators>

   * <https://docs.microsoft.com/en-us/dotnet/api/system.object.equals>

2. Poczytaj o implementowaniu interfejsu [`IEquatable<T>`](https://docs.microsoft.com/en-US/dotnet/api/system.iequatable), a w szczególności o metodzie [`IEquatable<T>.Equals(T)`](https://docs.microsoft.com/pl-pl/dotnet/api/system.iequatable).
  
3. _Równość_ jest [relacją równoważności](https://pl.wikipedia.org/wiki/Relacja_r%C3%B3wnowa%C5%BCno%C5%9Bci) (ściśle zdefiniowane pojęcie matematyczne), spełnia zatem następujące warunki:

    a. `x.Equals(x)` zwraca `true` //zwrotność

    b. `x.Equals(y)` zwraca tę samą wartość, co `y.Equals(x)` //symetria

    c. jeżeli `x.Equals(y)` zwraca `true` oraz `y.Equals(z))` zwraca `true`, wtedy `x.Equals(z)` zwraca `true` //przechodniość

    d. `x.Equals(y)` zwraca `true` jesli oba `x` oraz `y` są `NaN` (dla typów liczbowych)

    e. `x.Equals(null)` zwraca `false`

    Podane warunki przydadzą Ci się do opracowania testów jednostkowych Twojej implementacji równości ułamków.

   > Info: Dwa ułamki są sobie równe, jeżeli należą do tej samej _klasy równoważności_ względem relacji równości (też pojęcie _stricte_ matematyczne). Oznacza to, że np. 2/4 == 1/2. Ale tym problemem nie musimy się przejmować, ponieważ nasza implementacja przewiduje upraszczanie ułamków. Stąd, porównywanie ułamków może odbyć się _pole po polu_, jak domyślnie dla typów strukturalnych.

4. Prawa logiki przydają się przy budowaniu testów jednostkowych. Przykładowo, dla testu przechodniości relacji `Equals`:

    > jeżeli `x.Equals(y)` zwraca `true` oraz `y.Equals(z))` zwraca `true`, wtedy `x.Equals(z)` zwraca `true`

    W logice, zdanie ( (p ⇒ q) ⇔ (¬p ∨ q) ) jest tautologią - czyli zawsze prawdziwe. Zatem, zamiast używać implikacji, możemy ją zastapić odpowiednią alternatywą.

    W naszym przypadku, zdanie `(a ∧ b ⇒ c)`, gdzie `a` oznacza `x.Equals(y)`, `b` oznacza `y.Equals(z)`, zaś `c` oznacza `x.Equals(z)` zapiszemy w równoważny sposób, bez użycia implikacji: `¬(a ∧ b) ∨ c`.

    Korzystając z prawa de Morgana, przekształcimy je w `¬a ∨ ¬b ∨ c`.

    Pozostaje w teście sprawdzić, czy dla dowolnych danych testowych zdanie to jest zawsze prawdziwe. Jeśli nie - to powodem bedzie wadliwa implementacja `Equals`.

    ```csharp
    [DataTestMethod]
    [DataRow(1, 2, 1, 2, 1, 2)]
    [DataRow(1, 2, 2, 4, 3, 6)]
    [DataRow(-1, 2, 2, -4, -3, 6)]
    [DataRow(1, 2, -1, 2, 1, 2)]
    [DataRow(1, 2, 1, 3, 1, 3)]
    [DataRow(1, 2, 1, 2, 1, 3)]
    public void Equals_Przechodniosc_ZPrawLogiki_DowolneDane(int u1l, int u1m, int u2l, int u2m, int u3l, int u3m)
    {
        Ulamek x = new Ulamek(u1l, u1m);
        Ulamek y = new Ulamek(u2l, u2m);
        Ulamek z = new Ulamek(u3l, u3m);

        Assert.IsTrue( !x.Equals(y) || !y.Equals(z) || x.Equals(z) );
    }
    ```

5. Przeanalizuj i ustal różnice między trzema sposobami porównywania:

    * `object.ReferenceEquals(object objA, object objB)`
    * `objA.Equals(objB)`
    * `objA == objB`

6. Co to jest [`NullReferenceException`](https://docs.microsoft.com/pl-pl/dotnet/api/system.nullreferenceexception). Kiedy się może pojawić?

7. Przeanalizuj porównywanie do `null`:
    * `if( x == null ) ...`
    * `if( x is null ) ...`
    * `if( x.Equals(null) ) ...`
    * `if( object.ReferenceEquals( x, null ) ...`

    Zwróć uwagę na ukryte niebezpieczeństwa.

8. Porównywać możesz obiekty tego samego typu (czasami dopuszczalne dla podtypu). Jak sprawdzić, czy dwa obiekty są tego samego typu?

    Przeanalizuj poniższy kod:

    ```csharp
    //jakie są różnice (czy są)
    if (!(obj is Ulamek)) return false;
    if (obj.GetType() != typeof(Ulamek)) return false;
    if (this.GetType() != obj.GetType()) return false;
    if (!object.ReferenceEquals(this.GetType(), obj.GetType())) return false;
    ```

9. Dodatkowo (częste pytania egzaminacyjne):
  
    * jaka jest różnica między operatorem `is` oraz `as`
    * co zwraca operator `typeof`, o co metoda `GetType()`
    * czym jest `Type`

[Początek](README.md) | [Krok poprzedni](step01.md) | [Krok następny](step03.md)
# NGEnum

1.Definição: 
  - O pacote NGEnum contém uma estrutura com classes dinâmicas abstratas que servem para ser usadas como enums.

2.Vantagens: 
-   Ao contrário de Enum tradicionais a NGEnum pode ser herdado, a classe pai tem vários métodos para tratamentos de comparação.

# Documentação

### Implementação

Para criar seus enums, crie uma classe que herda da classe base(NGEnums) que contenha 3 construtores. Cada atributo será uma chave do seu enum, eles serão declarados como readonly e o tipo é a própria classe,  como mostrado no exemplo abaixo:
```ruby
public class NomeDoSeuEnum : NGEnums<NomeDoSeuEnum>
{
  public static readonly NomeDoSeuEnum ChaveDoEnum = new NomeDoSeuEnum("ChaveDoEnum");
  public static readonly NomeDoSeuEnum OutraChave = new NomeDoSeuEnum("OutraChave");

  public NomeDoSeuEnum() : base() { }
  public NomeDoSeuEnum(object pObject) : base(pObject) { }
  public NomeDoSeuEnum(int pId, object pObject) : base(pId, pObject) { }
}
```

Os atributos chaves podem ser declarados de algumas formas:
 - No primeiro exemplo será criado um id automático com o hash do objeto.
 - No segundo exemplo o id é declarado explicitamente. 
```ruby
  public static readonly NomeDoSeuEnum ChaveDoEnum = new NomeDoSeuEnum("ChaveDoEnum");
  public static readonly NomeDoSeuEnum ChaveDoEnum = new NomeDoSeuEnum(1,"ChaveDoEnum");
```

  - Como o construtor aceita um objeto, poderia ser inserido um objeto qualquer como chave, como outra classe, um type ou até mesmo um enum tradicional.
```ruby
public class NomeDoSeuEnum : NGEnums<NomeDoSeuEnum>
{
  public static readonly NomeDoSeuEnum Classe1 = new NomeDoSeuEnum(typeof(Classe1);
  public static readonly NomeDoSeuEnum Classe2 = new NomeDoSeuEnum(typeof(Classe2);

  public NomeDoSeuEnum() : base(None) { }
  public NomeDoSeuEnum(object pObject) : base(pObject) { }
  public NomeDoSeuEnum(int pId, object pObject) : base(pId, pObject) { }
}
```

Outra forma de criar um enum é herdar de outro enum já criado ao invés da base.
```ruby
public class HerdadoDeOutroEnum : NomeDoSeuEnum
{
  public static readonly NomeDoSeuEnum ChaveDoEnumHerdado = new NomeDoSeuEnum("ChaveDoEnumHerdado");
  public static readonly NomeDoSeuEnum OutraChaveHerdado = new NomeDoSeuEnum("OutraChaveHerdado");
}
```

Note que as propriedades são do tipo do enum pai, e a classe não precisa de construtor.

Como os enums podem ser herdados infinitamente, se necessário pode-se selar a classe para ela não poder ser mais herdada.
```ruby
public sealed class HerdadoDeOutroEnum : NomeDoSeuEnum
{
public static readonly NomeDoSeuEnum ChaveDoEnumHerdado = new NomeDoSeuEnum("ChaveDoEnumHerdado");
public static readonly NomeDoSeuEnum OutraChaveHerdado = new NomeDoSeuEnum("OutraChaveHerdado");
}
```

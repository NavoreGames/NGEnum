# NGEnum

1.Definição: 
  - O pacote NGEnum contém uma estrutura com classes dinâmicas abstratas que servem para ser usadas como enums.

2.Vantagens: 
-   Ao contrário de Enum tradicionais a NGEnum pode ser herdado, a classe pai tem vários métodos para tratamentos de comparação.

# Documentação

Para criar seus enums, crie uma classe que herda da classe base(NGEnums) que contenha 3 construtores. Cada atributo será uma chave do seu enum eles serão declarados como readonly e o tipo é a própria classe,  como mostrado no exemplo abaixo:


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

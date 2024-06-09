# Implantação do Software

### Implantação do Software
O sistema do Seu Embarque foi implantado com sucesso na plataforma SmarterAsp.Net , A escolha pelas plataformas de hospedagem proporcionou não apenas escalabilidade, mas também desempenho de maneira econômica. Já a base de dados, hospedada no Python Anywhere com MySQL, garante um ambiente seguro e escalável.

### Processo de Implantação
Para a hospedagem do banco de dados:

Criamos uma conta na plataforma Python Anywhere.
Foi configurada uma base de dados, armazenando a string de acesso gerada.
Utilizamos os scripts gerados pelo mySql para criar as tabelas necessárias.

Para hospedar o site:

Foi criado uma instancia do IIS para a aplicação no SmarterAsp.Net.
Geramos o arquivo publish pelo .NET e utilizamos a url gerada no SmarterAsp.Net para fazer a publicação na nuvem.

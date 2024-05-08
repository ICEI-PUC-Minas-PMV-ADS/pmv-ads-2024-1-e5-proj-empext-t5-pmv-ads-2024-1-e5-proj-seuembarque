# Plano de Testes de Software
Diante dos cenários apresentados e analisando os requisitos do projeto, foi realizado o plano de teste de software da aplicação. 

|*Caso de Teste      | *CT-001 – Formulário de informações            | 
|------------------|-------------------------------|
| Requisito Associado | RF-001 – A aplicação deve conter o formulário para preenchimento das informações do cliente | 
|Objetivo do Teste| Verificar se se o cliente cadastrado aparece na base de clientes da empresa seguindo os critérios de preenchimento do formulário.| 
|Passos   |1) Acessar a aplicação; 2) Visualizar a tela de “Informações do cliente”; 3) Preencher os campos com as informações solicitadas; 4) Clicar em “Enviar”.| 
| Critério de Êxito| Verificar na aba “Cliente” se o usuário foi adicionado com sucesso.|

|*Caso de Teste      | *CT-002 – Lista de aeroportos            | 
|------------------|-------------------------------|
| Requisito Associado | RF-002 - No campo Destino e Origem o campo deverá retornar uma lista com os aeroportos para cidade desejada pelo cliente. | 
|Objetivo do Teste| Verificar se após a inclusão dos dados nos campos Destino, Origem e Data a aplicação irá retornar os aeroportos mais próximos da cidade desejada.| 
|Passos   |1) Acessar a aplicação; 2) Visualizar a tela de “Destino”; 3) Verificar as opções de aeroportos próximo ao destino escolhido pelo cliente.| 
| Critério de Êxito| Verificar na aba “Destino” se a aplicação retornou as opções de aeroportos mais próximos ao destino do cliente.|

|*Caso de Teste      | *CT-003 – Informações adicionais            | 
|------------------|-------------------------------|
| Requisito Associado |RF-003 – O sistema deverá conter uma checkbox para verificar se o cliente deseja uma viagem com hospedagem, ao selecionar sim ele deve permitir que o cliente selecione se quer algo incluso como café da manha e almoço. | 
|Objetivo do Teste| Verificar se o cliente deseja uma viagem que contemple hospedagem com café da manhã, meia pensão ou pensão completa.| 
|Passos   |1) Acessar a aplicação; 2) Visualizar a tela “Monte o seu pacote de viagem!”; 3) Preencher o campo “Hospedagem?”; 4) Confirmar se deseja ou não hospedagem; 5) Selecionar o tipo de alimentação disponível; 6) Clicar em “Enviar”. | 
| Critério de Êxito| Verificar se as informações selecionadas pelos clientes foram registradas na base de informações do cliente.|

|*Caso de Teste      | *CT-004 – Informações via e-mail ou whatsapp            | 
|------------------|-------------------------------|
| Requisito Associado | RF-004 – A aplicação deve enviar via mensagem no Whatsapp ou Email às informações preenchidas pelo cliente. | 
|Objetivo do Teste| Verificar se houve retorno das informações registradas pelos clientes via Whatsapp ou e-mail.| 
|Passos   |1) Visualizar o email ou whatsapp da empresa; 3) Verificar se o orçamento solicitado pelo cliente encontra-se disponível para consulta.| 
| Critério de Êxito| Verificar se a mensagem estará disponível no e-mail ou Whatsapp da empresa.|

|*Caso de Teste      | *CT-005 – Informações do cliente            | 
|------------------|-------------------------------|
| Requisito Associado | RF-008 – O sistema deve possuir uma tela para que o administrador possa registrar clientes e editar os mesmos | 
|Objetivo do Teste| Verificar os dados dos clientes e realizar possíveis mudanças necessárias.| 
|Passos   |1) Acessar a aplicação; 2) Visualizar a tela “Clientes”; 3) Realizar as modificações necessárias dos clientes; 4) Salvar as informações.| 
| Critério de Êxito| Verificar se as modificações e novos registros foram realizados e salvos na base do administrador. |

|*Caso de Teste      | *CT-006 – Inserção de novo cliente        | 
|------------------|-------------------------------|
|Requisito associado: |RF-007| Deverá ser desenvolvido uma api para registro dos dados dos clientes e pacotes | ALTA |
|Objetivo do Teste| Verificar se o registro de clientes e está sendo executado corretamente. 
|Passos   |1) Acessar a aplicação; 2) Fazer o payload no Postman, usando o raw json, de acordo com o modelo client; 3) Digitar a url /cliente e selecionar o método POST; 4) Clicar em "send".| 
| Critério de Êxito| Um novo cliente deve ser inserido no banco de dados.|



|*Caso de Teste      | *CT-007 – Listagem de clientes         | 
|------------------|-------------------------------|
|Requisito associado: |RF-007| Deverá ser desenvolvido uma api para registro dos dados dos clientes e pacotes | ALTA |
|Objetivo do Teste| Verificar a listagem de clientes já inseridos no banco de dados. 
|Passos   |1) Acessar a aplicação; 2) Digitar a url /clientes e selecionar o método GET; 3) Clicar em "send"
| Critério de Êxito| Todos os clientes devem ser listados. |


|*Caso de Teste      | *CT-008 – Inserção de solicitação de novo pacote de viagem       | 
|------------------|-------------------------------|
|Requisito associado: |RF-007| Deverá ser desenvolvido uma api para registro dos dados dos clientes e pacotes | ALTA |
|Objetivo do Teste| Verificar a inserção de um novo pacote. 
|Passos   |1) Acessar a aplicação; 2) Digitar a url /pacote 3) Montar o payload, em raw json, de acordo com o modelo package e selecionar o método POST; 3) Clicar em "send"
| Critério de Êxito| Uma nova solicitação de pacote deve ser inserida no banco de dados. |

|*Caso de Teste      | *CT-009 – Filtragem de pacotes          | 
|------------------|-------------------------------|
|Requisito associado: |RF-007| Deverá ser desenvolvido uma api para registro dos dados dos clientes e pacotes | ALTA |
|Objetivo do Teste| Verificar se a filtragem de pacotes por múltiplos critérios está funcionando. 
|Passos   |1) Acessar a aplicação; 2) Digitar a pacotes/filtrar? seguido do critério desejado e selecionar o método GET; 3) Clicar em "send"
| Critério de Êxito| Todos os pacotes que correspondam aos critérios digitados devem ser listados. Os filtros são cumulativos. No caso de strings, são fornecidas todas os valores que possuam parte dos valores digitados, para facilitar a busca (operador %ILIKE%).|
 
# Evidências de Testes de Software

Apresente imagens e/ou vídeos que comprovam que um determinado teste foi executado, e o resultado esperado foi obtido. Normalmente são screenshots de telas, ou vídeos do software em funcionamento. 

## Registro das funcionalidades Front End

| Testes 	| CT-001 – Formulário de Informações |
|:---:	|:---:	|

![Formulário Cliente](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/cb71c30514ca92efdda5b536984db09e413fdff6/documentos/img/TesteHospedagem.png)

| Testes 	| CT-002 – Lista de Aeroportos (Origem/Destino)|
|:---:	|:---:	|

![Listagem de Origem](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/7f82d1ae46a4b79f82c0d74e497a87e33696ae1b/documentos/img/TesteOrigem.png)

![Listagem de Destino](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/7f82d1ae46a4b79f82c0d74e497a87e33696ae1b/documentos/img/TesteDestino.png)

| Testes 	| CT-003 – Informações Hospedagem |
|:---:	|:---:	|

![Teste de Hospedagem](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/7f82d1ae46a4b79f82c0d74e497a87e33696ae1b/documentos/img/TesteHospedagem.png)

| Testes 	| CT-004 – Informações via e-mail ou whatsapp |
|:---:	|:---:	|

![Teste de Whatsapp1](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/main/documentos/img/TesteWhatsapp1.png)

![Teste de Whatsapp2](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/main/documentos/img/TesteWhatsapp2.png)


| Testes 	| CT-005 – Informações do cliente |
|:---:	|:---:	|

|Acessar a aplicação e Visualizar a tela Clientes| 

![Tela de Cliente](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/main/documentos/img/teladeclientes.png)

|Realizar as modificações necessárias dos clientes|

![Usuário sem CPF](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/main/documentos/img/usuariosemcpf.png)

|Salvar as alterações|

![CPF Atualizado](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e5-proj-empext-t5-pmv-ads-2024-1-e5-proj-seuembarque/blob/main/documentos/img/cpfatualizado.png)





## Evidências de Testes de Software - Backend
|CT-006 – Inserção de novo cliente      |       | 
|:---:	|:---:	|
![alt text](img/Teste_post_client.png)

|CT-007 – Listagem de clientes      |         | 
|:---:	|:---:	|
![alt text](img/Teste_get_clientes.png)

|CT-008 – Inserção de solicitação de novo pacote de viagem      |       | 
|:---:	|:---:	|
![alt text](img/Teste_post_package.png)

|CT-009 – Filtragem de pacotes  |          | 
|:---:	|:---:	|
![alt text](img/Teste_get_package_filtros.png)


### Erros na construção da api
| Erro de circularidade Erro	| O erro mais importante a ser destacado na construção da api foi o de circularidade de importação. Isso ocorre quando o app gerado pela classe Flask, no app.py, é exportado para ser utilizado em outras classes. Como os modelos são importados para o app para construírem a aplicação, isso gerava circularidade, isto é, ao mesmo tempo que os modelos ou arquivos de blueprints (que são controllers) precisavam do app, o app precisava deles.   |
![alt text](img/TestesDeSoftwareBackendErros/erro_back_circularidade.png)

| Solução para o erro de circularidade	| A solução para esse erro foi dividir a aplicação em app, onde importamos os modelos e geramos o app por meio da classe Flask; modelos, onde usamos o SqlAlchemy para tipificar, estruturar e relacionar as tabelas de acordo com as regras de negócio; config, onde configuramos o banco de dados e exportamos apenas o db; e blueprints, que são uma forma de controller, no qual importamos os modelos e realizamos as operações de crud. 
![alt text](img/TestesDeSoftwareBackendErros/solucao_circularidade.png)



## Registro dos erros e soluções Backend (API-Python, Back C#)
### C#

| Toast Erro	| Um dos problemas que tivemos na implementação do toast de validação da tela de login é que não estavamos conseguindo fazer ele exibir a mensagem da resposta ao tentar logar |
|:---:	|:---:	|


![ErroToastLogin](/documentos/img/TestesDeSoftware/ErroToastLogin.png)

#### Solução

A solução encontrada para este erro foi colocar uma condição validando se a mensagem for diferente de nulo ele chamar a função pra exibição do erro onde essa função ativa o toast

![FrontSoluçãoToast](/documentos/img/TestesDeSoftware/FrontSoluçãoToast.png)

![FunctionErroToast](/documentos/img/TestesDeSoftware/FunctionErroToast.png) 


### Validação Login
Tive um erro na validação de login onde eu tentava chamar o endpoint para validar o usuário porem dava o erro MethodNotAllowed

![alt text](/documentos/img/TestesDeSoftware/image.png)

O erro ocorria pois no endpoint na api no python este metodo recebia o payload via body e eu estava usando o GetAsync do c# onde nao permite passar dados via body

![alt text](/documentos/img/TestesDeSoftware/image-1.png)

#### Solução
A solução para o problema foi mudar de GetAsync para PostAsync no c# 
![alt text](/documentos/img/TestesDeSoftware/image-2.png)

E mudar na api para receber chamadas POST 
![alt text](/documentos/img/TestesDeSoftware/image-3.png)

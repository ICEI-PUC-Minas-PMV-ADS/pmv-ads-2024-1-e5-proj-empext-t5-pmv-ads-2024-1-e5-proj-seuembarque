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
 
# Evidências de Testes de Software

Apresente imagens e/ou vídeos que comprovam que um determinado teste foi executado, e o resultado esperado foi obtido. Normalmente são screenshots de telas, ou vídeos do software em funcionamento. 

| Testes 	| CT 01 – Formulário de Informações |
|:---:	|:---:	|
![Teste de Formulário](img/Testes de Software/TesteHospedagem.png)

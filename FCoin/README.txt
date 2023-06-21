Fluxo a ser seguido

1 -> Criar pelo menos duas instâncias de Client - Post/Client

2 -> Criar pelo menos uma instância de Selector - Post/Selector

3 -> Criar pelo menos três instâncias de Validator - Post/Validator

4 -> Criar uma transação relacionada entre dois Clients - Post/Transaction

5 -> Selecionar os validadores para validar a transação - Post/Selector/SelectValidators

6 -> Validar a transação com os três validadores, passando o token e os Ids, podendo ser consultada quais as validações a serem feitas
	6.1 -> Ver qual a ultima transação a ser validada pelo validador - Get/Validator/LastTransactionToValidate

7 -> Após as 3 validações serem feitas, chechar o resultado da transação - Get/Transaction/CheckStatus 

8 -> Retornar ao passo 4
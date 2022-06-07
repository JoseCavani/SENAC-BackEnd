CREATE TABLE [dbo].[TBCOMPRA_E_VENDA] (
    [Numero_Produto] INT NOT NULL,
    [valor]          INT NULL,
    [Numero_Cliente] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Numero_Produto] ASC, [Numero_Cliente] ASC),
    CONSTRAINT [FK_TBCOMPRA_E_VENDA_ToTable] FOREIGN KEY ([Numero_Cliente]) REFERENCES [dbo].[TBCliente] ([Numero]),
    CONSTRAINT [FK_TBCOMPRA_E_VENDA_ToTable_1] FOREIGN KEY ([Numero_Produto]) REFERENCES [dbo].[TBEstoque] ([Numero])
);


---------------------------------------------------------------------------------------------------
--���������� ����� � 1340 �� 46 ������ � ����� � 33�� �� 5�� (������������� ���������� �������� �� 3,5 ������� � 0,5)
CREATE NONCLUSTERED INDEX IX_TransferRecords_FindComponents
ON dbo.TransferRecords (DestinationObjectID,DestinationObjectType,ParentID,ParentType,IsDeleted);

---------------------------------------------------------------------------------------------------
CREATE NONCLUSTERED INDEX IX_ComponentDirectives_FindComponent ON [dbo].[ComponentDirectives]
(
	[ComponentId] ASC,
	[IsDeleted] ASC
)
INCLUDE ( 	[ItemID],
	[DirectiveType],
	[Threshold],
	[ManHours],
	[Remarks],
	[Cost],
	[Highlight],
	[KitRequired],
	[FaaFormFileID],
	[JobCardsID],
	[EOFileID],
	[HiddenRemarks],
	[IsClosed],
	[MPDTaskTypeId],
	[NDTType],
	[ZoneArea],
	[AccessDirective],
	[AAM],
	[CMM],
	[Corrector],
	[Updated],
	[ExpiryDate],
	[ExpiryRemainNotify],
	[IsExpiry])
GO
---------------------------------------------------------------------------------------------------
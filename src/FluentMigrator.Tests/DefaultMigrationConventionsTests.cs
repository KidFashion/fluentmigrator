﻿using System;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Model;
using Xunit;

namespace FluentMigrator.Tests
{
	public class DefaultMigrationConventionsTests
	{
		[Fact]
		public void GetPrimaryKeyNamePrefixesTableNameWithPKAndUnderscore()
		{
			var expression = new CreateTableExpression { TableName = "Foo" };
			Assert.Equal("PK_Foo", DefaultMigrationConventions.GetPrimaryKeyName(expression));
		}

		[Fact]
		public void GetForeignKeyNameReturnsValidForeignKeyNameForSimpleForeignKey()
		{
			var foreignKey = new ForeignKeyDefinition
			{
				ForeignTable = "Users", ForeignColumns = new[] { "GroupId" },
				PrimaryTable = "Groups", PrimaryColumns = new[] { "Id" }
			};

			Assert.Equal("FK_Users_GroupId_Groups_Id", DefaultMigrationConventions.GetForeignKeyName(foreignKey));
		}

		[Fact]
		public void GetForeignKeyNameReturnsValidForeignKeyNameForComplexForeignKey()
		{
			var foreignKey = new ForeignKeyDefinition
			{
				ForeignTable = "Users", ForeignColumns = new[] { "ColumnA", "ColumnB" },
				PrimaryTable = "Groups", PrimaryColumns = new[] { "ColumnC", "ColumnD" }
			};

			Assert.Equal("FK_Users_ColumnA_ColumnB_Groups_ColumnC_ColumnD", DefaultMigrationConventions.GetForeignKeyName(foreignKey));
		}
	}
}

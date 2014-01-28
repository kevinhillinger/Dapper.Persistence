Dapper.Persistence
================

dapper-dot-net persistence library. Simple. light. 

data context + repository + unit of work. that simple.

## What's this for?

This isn't meant to be a replacement for an ORM.  This isn't about a large framework. This is about providing a data context for a repository without directly depending on an IDbConnection, then use the semantics of unit of work to have transactions in your business services.

## Why did you wrap dapper?

If Dapper gets an update, you get the update. I'm not rewriting dapper.

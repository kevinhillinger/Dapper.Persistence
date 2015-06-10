Dapper.Persistence
================

dapper-dot-net persistence library. Simple. light. 

data context + repository + unit of work. that simple.

## What's this for?

This isn't meant to be a replacement for an ORM.  This isn't about a large framework. This is about providing a data context for a repository without directly depending on an IDbConnection, then use the semantics of unit of work to have transactions in your business services.

## Why did you wrap dapper?

First off, if Dapper gets an update, you get the update. I'm not rewriting dapper. 

Second, it comes down to a belief thing for me: I don't believe
that IDbConnection should be a direct dependency because too much can go wrong, particularly when sharing that connection. If that
that on a project where Dapper was being used, IDbConnection was being abused.



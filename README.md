Dapper.Mousse
================

dapper-dot-net persistence library. Simple. light. 

data context + repository + unit of work. that simple.

## What's this for?

This isn't meant to be a replacement for an ORM.  This isn't about a large framework. This is about providing a data context for a repository without directly depending on an IDbConnection, and using a unit of work with a connection semantically.

## Why did you wrap dapper?

If Dapper gets an update, you get the update. I'm not rewriting dapper.

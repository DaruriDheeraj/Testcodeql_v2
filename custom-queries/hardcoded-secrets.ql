import csharp

from VariableDeclarator v
where v.getName().matches("(?i).*(api|key|password|secret|token|cred).*")
select v, "🚨 Hardcoded credential detected: " + v.getName()

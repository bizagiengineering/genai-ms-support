# GPT-4o Truncated Response Investigation

This repository contains a minimal .NET 8 console application used to isolate and reproduce an issue when calling a deployed Azure OpenAI model (`gpt-4o`). The purpose of this project is to provide evidence and allow the Azure support team to further analyze the behavior.

## 🧠 Problem Description

We are currently experiencing a consistent issue where the responses from the `gpt-4o` model are truncated or incomplete, despite not hitting any known token limits or rate quotas. Below are key details of the environment and how the issue manifests:

- **NuGet Package:** `Azure.AI.OpenAI` version `2.1.0`
- **Model Deployment:** Azure AI Foundry  
  - **Model name:** `gpt-4o`  
  - **Version:** `2024-11-20`  
  - **Deployment Type:** Global Standard  
  - **Capacity:** 450  
  - **Rate limit (tokens per minute):** 450,000  
  - **Rate limit (requests per minute):** 2,700

### 🔍 Behavior Observed

- A prompt is constructed with the following structure:
  1. A **system message** instructing the model to use the following context.
  2. A **second system message** that contains the context retrieved from a RAG system (approximately 8,000 tokens).
  3. A **user message** with a natural language question (approx. 35 tokens).
- The `ChatCompletionsOptions` are configured with `MaxTokens=16384` (maximum allowed).
- Despite this, responses are often truncated before reaching this token limit.
- This issue occurs even when:
  - Using a **dedicated model deployment** to eliminate quota or concurrency issues.
  - Azure usage metrics show **no token exhaustion or rate limit violations**.
  - Varying or omitting the `MaxTokens` parameter has **no effect** on the outcome.
- Output diagnostics from the model (shown in console):
  - `InputTokens`, `OutputTokens`, and `TotalTokens` are always well within allowed limits.
  - Yet, the model still frequently returns **incomplete responses**.

### 📁 Evidence

You can find screenshots demonstrating the issue in the following folder:

ErrorEvidence/GPT4oResposeTruncatiosEvicende

These images show side-by-side:
- Model responses
- Token counts
- Reproducibility of the truncation

---

## ▶️ How to Run the Project

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A deployed Azure OpenAI `gpt-4o` model with access credentials.

### 1. Set Your Credentials

You can provide your Azure OpenAI credentials in either of two ways:

#### Option A: `secrets.json` (Recommended)

Create a `secrets.json` file with the following structure:

```json
{
  "OpenAI": {
    "Uri": "<your-uri-here>",
    "Key": "<your-key-here>"
  }
}
```

This file must be located in the correct secrets path for .NET user secrets, or adapted into your preferred configuration method.

Option B: Inline in Code
If desired, you can directly edit the variables in the main program file:

string openAIUri = "<your-uri-here>";
string openAIKey = "<your-key-here>";

⚠️ Important: Make sure not to commit real keys to any public repository.

### 2. Run the App

Open a terminal in the project directory and run:

dotnet run

The console will display:

- The prompt structure

- Token usage statistics

- The raw response from the model

🧪 Purpose

This minimal reproduction is meant to:

Demonstrate that the model truncates responses despite valid input and sufficient token limits.

Show that the issue is reproducible under controlled conditions.

Assist Azure support in identifying root cause or misconfiguration if any.

If further details, logs, or additional reproduction scenarios are required, please contact the project maintainer.
# genai-ms-support

This repository contains isolated, minimal reproductions of issues encountered while working with Generative AI services in Microsoft Azure. It is intended to assist Microsoft support in analyzing and resolving technical problems by providing clear and verifiable test cases.

---

## 📌 Current Case

### GPT-4o Truncated Response Issue

We are currently investigating an issue where the Azure OpenAI `gpt-4o` model frequently returns truncated or incomplete responses, despite all parameters and usage remaining within the allowed limits.

An isolated .NET 8 console application has been created to reproduce this issue.

📂 **Path to project:**  

/Issues/GPT4oTruncatedResponse

📄 **Details and reproduction steps:**  
See the [README in the project folder](./Issues/LLMResponseTruncationScenario/README.md)

---

## 📁 Structure

- `/Issues` – Contains individual test projects for specific issues  
- `README.md` – Overview of repository and tracked support cases

---

## 🔄 Contributing

This repo is internal to our engineering team for the purpose of coordinating support efforts with Microsoft. Each issue folder includes its own documentation and setup for reproducibility.

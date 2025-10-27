# Work_Sample
Code snippets showcasing my programming skills

This is a simplified, non-confidential demo version of a user based website I built 
during my professional experience. All company-specific data has been removed.

# Document Management Utility – Group Policy Portal

## 🧩 Overview
This .NET-based internal utility was developed to simplify document access for **Operations users** handling **Group Policy Holders**.  
The system provides a unified interface to **view and download** documents such as Certificates, Endorsements, and Service Requests for any specific Member ID.

The key objective was to **enhance operational efficiency** by reducing manual lookup time and ensuring secure, quick retrieval of member-related documents.

---

## 🧠 Core Features

### 1️⃣ Tree Hierarchical View
- Displays all related documents (Certificates, Endorsements, Service Requests) in a **tree-structured format**.
- Each node expands into subcategories like *Basic Details*, *Endorsement Details*, and *E-Cards*.
- Users can easily identify document groups and navigate between them visually.

### 2️⃣ Document Access & Download
- Each listed file in the tree view is **clickable**, allowing users to download the document instantly.
- Supported multiple formats such as **PDF** for policy certificates and schedules.

### 3️⃣ Member-Based Search
- Operation users can enter a **Member ID** to fetch all associated documents dynamically.
- Eliminated the need to manually locate files from internal directories.

---

## 🏗️ Technical Implementation

| Component | Description |
|------------|--------------|
| **Technology Stack** | ASP.NET / C# (.NET Framework 4.6.2) |
| **Frontend** | ASP.NET Web Forms |
| **Backend** | C# (Business Logic Layer) |
| **Database** | SQL Server |
| **UI Elements** | TreeView Control for hierarchical visualization |
| **File Handling** | Dynamic path resolution and file streaming for downloads |

---

## 🔧 How It Works

1. **User Input** – Operations team enters a valid **Member ID**.  
2. **Fetch Process** – System queries the document repository for all records linked to that Member ID.  
3. **Tree View Generation** – Documents are grouped by categories (Basic Details, Endorsement Details, etc.) and displayed hierarchically.  
4. **Download Option** – Clicking on any document name triggers a **secure download** through the backend handler.

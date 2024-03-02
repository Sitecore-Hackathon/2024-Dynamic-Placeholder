![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2024

## Team name
⟹ Dynamic Placeholder

## Category
⟹ 1 AI

## Description
⟹ The AI Translator module for Sitecore 10.3 addresses the challenge of efficiently creating and managing multilingual content within the Sitecore CMS, focusing on English to Spanish translations. This is a critical requirement for businesses and organizations that want to cater to a diverse audience, ensuring that their digital content is accessible and relevant to people speaking different languages. Manual translation processes are time-consuming, resource-intensive, and prone to inconsistencies, limiting the ability to quickly update and deploy content across multiple market segments.

The module automates the translation of content items from English to Spanish, saving significant time and effort when managing multilingual content. It integrates with OpenAI's GPT-3 technology to ensure high-quality translations that maintain the original content's tone and context.



## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Video link](https://youtu.be/mbWWG6yUkhg)



## Pre-requisites and Dependencies
- Newtonsoft.Json
- HttpClient
- Open IA valid Token


## Installation instructions
### 1. Upload Package

- Download the `AITranslator-7.7.7.zip` file provided for the AI Translator feature.
- Log into the Sitecore Desktop.
- Open the Development Tools and select the Installation Wizard.
- Upload the `package.zip` through the Installation Wizard and follow the on-screen instructions to install.

### 2. Configure API Token

- Navigate to `/sitecore/system/Modules/AITranslator` in the Content Editor.
- Create a new item of the type `API Key`.
- Enter your OpenAI API Token in the `API Key` field of the newly created item.

### Configuration
⟹ If there are any custom configuration that has to be set manually then remember to add all details here.

_Remove this subsection if your entry does not require any configuration that is not fully covered in the installation instructions already_

## Usage instructions
This module is designed to automatically create a Spanish version of content items, exclusively within the master database. It copies all fields and renderings from the English version to the Spanish version, translating only the fields of types: Single-Line Text, Multiline Text, and Rich Text.

To use the AI Translation feature:

- Navigate to the content item you wish to translate in the Content Editor.
- Right-click on the item and select "AI Translation" from the context menu.
- Choose "EN to ES" to initiate the translation from English to Spanish.
- An alert box will appear, guiding you through the steps of the translation process.

## Developer Notes

- Ensure the OpenAI API Token is securely stored and access is restricted to authorized personnel.
- Monitor API usage to stay within quota limits and manage costs.
- Test translations for accuracy and adjust prompts as necessary to improve results.
- The module aims to facilitate seamless content localization efforts, streamlining the translation process within Sitecore environments.

## Features Overview

The AI Translator module integrates OpenAI's GPT-3 technology with Sitecore, enabling automatic translation of content items from English to Spanish. It consists of three main components:

### 1. AITranslateEngToSpa Command

- **Purpose:** Initiates the translation process for a selected content item from English to Spanish.
- **Usage:** Accessible from the context menu of a content item. When executed, it checks for the selected item and triggers the translation pipeline if conditions are met.
- **Parameters:**
  - `database`: The database of the selected item.
  - `id`: The ID of the selected item.
  - `name`: The name of the selected item.

### 2. AITranslateEngToSpaPipeline

- **Purpose:** Executes the translation process, interfacing with the OpenAI GPT-3 API to translate content fields.
- **Usage:** Automatically called by the `AITranslateEngToSpa` command. It retrieves the item based on parameters, checks for the existence of both English and Spanish versions, and performs field-wise translation where applicable.
- **Key Operations:**
  - Validation checks for database and item existence.
  - Retrieval of English and Spanish item versions.
  - Translation of text fields using the `ChatGPTApiClient`.
  - Updates the Spanish version with translated content and layout.

### 3. ChatGPTApiClient

- **Purpose:** Facilitates communication with the OpenAI GPT-3 API to translate text.
- **Usage:** Utilized by the `AITranslateEngToSpaPipeline` to send prompts for translation and receive translated text.
- **Configuration:**
  - Retrieves the OpenAI API Token from a Sitecore item.
  - Constructs and sends HTTP requests with prompts to the OpenAI API.
  - Parses and returns the API response.

## Comments
This module can be used to translate to several languages.

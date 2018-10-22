# WinHost

Note: This repository is not affiliated with Mark, Sysinternals, or Microsoft.

## What is this?

This is a replica version of the application called the "Windows Host Support Service" (also known as "winhost" or "buddy") that was used in the Malware Hunting with the Sysinternals Tools talks. It is used to demonstrate properties of malware and some characteristics that they show?

## What characteristics are we looking for?

From the PowerPoint, characteristics to look for are processes that:
* ...have no icon.
* ...have no description or company name.
* ...unsigned Microsoft images.
* ...live in Windows directory or user profile.
* ...are packed.
* ...include strange or suspicious URLs in their strings.
* ...have open TCP/IP endpoints.
* ...host suspicious DLLs or services.

The "Windows Host Support Service" has some of these characteristics including:
* Unsigned Microsoft Image
* Strange/suspicious URLs ("a suspicious url")
* Packed (if packed with UPX on compilation)
* Live in Windows directory (if "installed").

Additionally, it has the following characteristics:
* It uses the old icon used in `winlogon.exe` before Windows 8.
* It has a company name of Microsoft Corporation.
* It's description is the "Windows Host Support Service".

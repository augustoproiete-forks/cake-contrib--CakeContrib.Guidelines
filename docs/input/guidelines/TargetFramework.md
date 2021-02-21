---
Order: 4
Title: Target Frameworks
---

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Goals](#goals)
  - [Required / Suggested versions](#required--suggested-versions)
- [Related rules](#related-rules)
- [Usage](#usage)
- [Settings](#settings)
  - [Opt-Out](#opt-out)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Goals

Each addin should have maximum compatibility when being used. Toward that end some Framework versions are required and some others are 
suggested, depending on the Cake.Core version that is being referenced. 

### Required / Suggested versions

Depending on the referenced `Cake.Core`-version different target versions are required and/or suggested.
Missing a required target version will raise [CCG0007](../rules/ccg0007) as an error
while missing a suggested target version will raise [CCG0007](../rules/ccg0007) as a warning.

* Cake.Core < 1.0.0
  * Required: `netstandard2.0`
  * Suggested: `net461`
    * alternative: `net46`
* Cake.Core >= 1.0.0
  * Required: `netstandard2.0`
  * Suggested: `net461`
    * alternative: `net46`
  * Suggested: `net5.0`

## Related rules

 * [CCG0007](../rules/ccg0007)

## Usage

Using this package automatically enables this guideline.

## Settings

### Opt-Out

It it possible to opt-out of the check for target framework using the following setting:

(*Keep in mind, though that it is not recommended to opt-out of this feature*)

```xml
<ItemGroup>
    <CakeContribGuidelinesOmitTargetFramework Include="netstandard2.0" />
    <CakeContribGuidelinesOmitTargetFramework Include="net461" />
</ItemGroup>
```

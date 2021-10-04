* [x] The app complies with the [inclusion criteria](https://f-droid.org/docs/Inclusion_Policy/?title=Inclusion_Policy).
* [x] The app is not already [listed](https://gitlab.com/search?scope=issues&group_id=28397) in the repo or issue tracker.
* [x] The app has not already [been requested](https://gitlab.com/search?scope=issues&project_id=2167965)
* [x] The upstream app source code repo contains the app metadata _(summary/description/images/changelog/etc)_ in a [Fastlane](https://gitlab.com/snippets/1895688) or [Triple-T](https://gitlab.com/snippets/1901490) folder structure
* [x] The original app author has been notified, and does not oppose the inclusion. **I am the autor**
* [ ] [Donated](https://f-droid.org/donate/) to support the maintenance of this app in F-Droid.

---------------------

The first step is to find the app's
[_Application ID_](https://developer.android.com/studio/build/application-id.html).
This is usually the same as the app's _Package Name_. You will find it
in files called _AndroidManifest.xml_ or _build.gradle_ most of the
time. You can also see it in the URLs for the app's page in various
app stores. Write it here:

#### APPLICATION ID: SnakeMobile.SnakeMobile

Below is a template "metadata file" to fill out, it has only the
required fields.  F-Droid uses this file to build and publish the app.
[Build Metadata Reference](https://f-droid.org/docs/Build_Metadata_Reference)
documents all available options. Add values after the colon


```yaml
# Categories (one per line, each starting with a space and a minus), chosen from the
# official list: https://gitlab.com/fdroid/fdroiddata/blob/master/stats/categories.txt
Categories:
 - Games

# the one license that the whole app is available under, use
# https://spdx.org/licenses/ short identifiers, must be
# floss-compatible.
License: GPL-3.0

# You can provide details on how to contact the author. These are optional, but
# nice to have.
AuthorName: Edouard
AuthorEmail: optimusvenatus1234@gmail.com
AuthorWebSite: https://github.com/OptimusVenatus

# A URL for the project's website, and to the source code repository to visit
# using a web browser. WebSite is optional.
WebSite: 
SourceCode: 

# A link to the issue tracker where bugs are reported
IssueTracker: https://github.com/OptimusVenatus/CyberSnake/issues

# If available, you can also provide links/IDs for donations.
Donate: 
Bitcoin: 
LiberaPay: 

# Name of the application
AutoName: Cyber Snake

# One sentence, no more than 30-50 chars, no trailing punctuation,
# focus on actions what the users does with the app, e.g. "Read and
# send emails" instead of "Email client".
# NOTE: Summary and Description are preferably provided via Fastlane or Triple-T!
Summary: 
Description: |-
    Cyebrsnake is a 72h coding challenge from 2021-22-03.

    It's my remake of the original snake game with brand new style 

# Repository details to be used by VCS (Version Control Systems)
# git, git-svn, svn, hg or bzr
RepoType: git

# source code repo URL (HTTPS required)
Repo: https://github.com/OptimusVenatus/CyberSnake
```
# Diagramme de flux pour la checklist `Chaudiere`
``` mermaid
---
config:
  flowchart:
    htmlLabels: false
    defaultRenderer: "elk"
---
flowchart TD

%%Déclaration des noeuds

  debut(((Début)))
  fin1(((Fin)))
  fin2(((Fin)))
  fin3(((Fin)))
  fin4(((Fin)))
  etape_Choix_TypeIntervention["Choix du type d'intervention **Val_TypeIntervention**"]
  etape_MNT_Saisie_CommentaireTech["Saisie du commentaire du technicien **Val_CommentaireTech**"]
  etape_MNT_Saisie_CommentaireEntreprise["Saisie du commentaire pour l'entreprise **Val_CommentaireEntreprise**"]
  etape_MNT_Saisie_CommentaireClient["Saisie du commentaire du client **Val_CommentaireClient**"]
  etape_MNT_SignatureClient["Signature du client **Val_SignatureClient**"]
  etape_MNT_Saisie_TempsIntervention["Saisie du temps de l'intervention :
  **Val_HeureDebutIntervention**
  **Val_HeureFinIntervention**"]

  etape_MES_Choix_TypeChaudiere["Choix du type de chaudière **Val_TypeChaudiere**"]
  etape_MES_IdentificationChaudiere[["Saisie des informations de la chaudière
    **Val_MarqueChaudiere**
    **Val_TypeChaudiere**
    **Val_NumeroSerie**
    **Val_TypeFumisterie**
    **Val_PuissanceChaudiere**
    **Val_Localisation**"]]
  etape_MES_PhotoPlaqueSignaletique["Photo de la plaque signalétique **Val_PhotoPlaqueSignaletique**"]
  etape_MES_PhotoEnsemble["Photo de l'ensemble de l'installation **Val_PhotoEnsemble**"]
  etape_MES_CombustibleGaz["Choix du type de combustible gaz **Val_TypeCombustible**"]
  etape_MES_CombustibleFioul["Choix du type de combustible fioul **Val_TypeCombustible**"]
  etape_MES_CombustibleBois["Choix du type de combustible bois **Val_TypeCombustible**"]
  etape_MES_InstallationGaz[["Saisie des informations de l'installation gaz
    **Val_TypeInstallation**
    **Val_Verif_Flexible**
    **Val_Verif_ROAI**
    **Val_Verif_Diametre**
    **Val_Verif_Longueur**"]]
  etape_MES_SignatureTechnicien["Signature du technicien **Val_SignatureClient**"]

  etape_MNT_SaisieCO["Saisie du CO **Val_TauxCO**"]
  etape_MNT_SaisieCOInvalide>"Taux CO invalide"]
  etape_MNT_SaisieCOAerer>"Aérer et refaure une mesure du taux de CO"]
  etape_MNT_SaisieCODanger>"Couper l'installation et appeler l'entreprise"]
  etape_MNT_Choix_TypeChaudiere["Saisie du type de chaudière **Val_TypeChaudiere**"]
  etape_MNT_VerifChaudiere["Validation des informations de la chaudière **Val_ModifierInfoChaudiere**"]
  etape_MNT_IdentificationChaudiere[["Saisie des informations de la chaudière
    **Val_MarqueChaudiere**
    **Val_TypeChaudiere**
    **Val_NumeroSerie**
    **Val_TypeFumisterie**
    **Val_PuissanceChaudiere**
    **Val_Localisation**"]]
  etape_MNT_CombustibleGaz["Choix du type de combustible gaz **Val_TypeCombustible**"]
  etape_MNT_CombustibleFioul["Choix du type de combustible fioul **Val_TypeCombustible**"]
  etape_MNT_CombustibleBois["Choix du type de combustible bois **Val_TypeCombustible**"]

  etape_MNT_PhotoEnsemble["Photo de l'ensemble de l'installation **Val_PhotoEnsemble**"]
  etape_MNT_PhotoPlaqueSignaletique["Photo de la plaque signalétique **Val_PhotoPlaqueSignaletique**"]
  etape_MNT_VerificationAvantIntervention[["Etape de vérification avant entretien
    **Val_EtatChaudiere**
    **Val_PressionChauffageAvantIntervention**
    **Val_FonctionnementChauffageAvantIntervention**
    **Val_FonctionnementECSAvantIntervention**"]]
  etape_MNT_VerificationSecuriteChaudiere["Etape de vérification  des sécurités de la chaudière **Val_VerificationSecuriteChaudiere**"]
  etape_MNT_SecuriteAquastat["Vérification de l'aquastat limiteur **Val_Securite_Aquastat**"]
  etape_MNT_SecuriteControleurDebit["Vérification du contrôleur de débit **Val_Securite_ControleurDebit**"]
  etape_MNT_SecuriteControleurPression["Vérification du contrôleur de pression **Val_Securite_ControleurPression**"]
  etape_MNT_SecuriteSondeIonisation["Vérification de la sonde d'ionisation **Val_Securite_SondeIonisation**"]
  etape_MNT_SecuriteSoupape["Vérification de la soupape **Val_Securite_Soupape**"]
  etape_MNT_SecuriteSPOTT["Vérification du SPOTT **Val_Securite_SPOTT**"]
  etape_MNT_SecuriteThermocouple["Vérification du Thermocouple **Val_Securite_Thermocouple**"]
  etape_MNT_SecuriteDSI["Vérification du DSI **Val_Securite_DSI**"]
  etape_MNT_SecuriteDSC["Vérification du DSC **Val_Securite_DSC**"]
  etape_MNT_SecuritePressostatDifferentiel["Vérification du pressostat differentiel **Val_Securite_PressostatDifferentiel**"]
  etape_MNT_VerificationNettoyage[["Etape de vérification et nettoyage
    **Val_VerificationEtatJoints**"
    **Val_VerificationNettoyageCorps**
    **Val_VerificationNettoyageFiltres**
    **Val_VerificationQualiteEau**
    **Val_PressionVaseAvantIntervention**
    **Val_PressionVaseApresIntervention**]]
  etape_MNT_Combustion1[["Pression Gaz
    **Val_PressionGazArret**
    **Val_PressionGazPuissanceMax**
    **Val_PressionGazRampe**"]]
  etape_MNT_Combustion2[["Pression Gaz et combustion à puissance maximale
    **Val_PressionGazArret**
    **Val_PressionGazPuissanceMax**
    **Val_TauxCOPuissanceMax**
    **Val_TauxO2PuissanceMax**
    **Val_TauxCO2PuissanceMax**"]]
  etape_MNT_Combustion3[["Pression Gaz et combustion à puissance minimale
    **Val_PressionGazPuissanceMin**
    **Val_TauxCOPuissanceMin**
    **Val_TauxO2PuissanceMin**
    **Val_TauxCO2PuissanceMin**"]]
  etape_MNT_VerificationApresIntervention[["Vérification après intervention
    **Val_PressionChauffageApresIntervention**
    **Val_FonctionnementChauffageApresIntervention**
    **Val_FonctionnementECSApresIntervention**
    **Val_PressionEFApresIntervention**"]]

  subgraph Legende ["**Légende**"]
    legende_message>"Message"]
    legende_saisieunique["Saisie unique"]
    legende_saisiemultiple[["Saisie multiple"]]
  end

  subgraph Useless ["**Noeuds inutilisés**"]
    etape_Inutilise_InformationInstallationMES[InformationInstallationMES]
    etape_Inutilise_Etape1Circuit[Etape1Circuit]
    etape_Inutilise_SignatureTechnicienReferencement[SignatureTechnicienReferencement]
    etape_Inutilise_Etape1TestYOBL[Etape1TestYOBL]
    etape_Inutilise_Themostat[Themostat]
  end


%%Déclaration des liens

  debut --> etape_Choix_TypeIntervention
  etape_Choix_TypeIntervention -->|Val_TypeIntervention = Ref_TestYOBL|etape_MNT_Saisie_CommentaireTech
  etape_Choix_TypeIntervention -->|Val_TypeIntervention = Ref_MiseEnService|etape_MES_Choix_TypeChaudiere
  etape_Choix_TypeIntervention -->|Val_TypeIntervention = Ref_ReferencementEquipement|etape_MES_Choix_TypeChaudiere
  etape_Choix_TypeIntervention -->|Val_TypeIntervention = Ref_ReferencementEquipement ET Val__TypeChaudiere EST INDEFINI|etape_MES_Choix_TypeChaudiere
  etape_Choix_TypeIntervention -->etape_MNT_SaisieCO

  subgraph MES ["**Mise en service**"]
    etape_MES_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_GazMural| etape_MES_CombustibleGaz --> etape_MES_IdentificationChaudiere
    etape_MES_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_GazAuSol| etape_MES_CombustibleGaz
    etape_MES_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_Fioul| etape_MES_CombustibleFioul --> etape_MES_IdentificationChaudiere
    etape_MES_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_Bois| etape_MES_CombustibleBois --> etape_MES_IdentificationChaudiere
    etape_MES_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_BalonECSGaz| etape_MES_CombustibleGaz
    etape_MES_Choix_TypeChaudiere --> etape_MES_IdentificationChaudiere
    etape_MES_IdentificationChaudiere --> etape_MES_PhotoPlaqueSignaletique --> etape_MES_PhotoEnsemble --> etape_MES_InstallationGaz --> etape_MES_SignatureTechnicien --> fin3
  end

  subgraph MNT ["**Maintenance**"]
    etape_MNT_SaisieCO --> etape_MNT_SaisieCOInvalide --> fin2
    etape_MNT_SaisieCO -->|Val_TauxCO = Ref_TauxCO_AucunDanger ET Val_TypeCombustible EST INDEFINI| etape_MNT_Choix_TypeChaudiere
    etape_MNT_SaisieCO -->|Val_TauxCO = Ref_TauxCO_AucunDanger ET Val_TypeCombustible EST DEFINI| etape_MNT_VerifChaudiere
    etape_MNT_SaisieCO -->|Ref_TauxCO_AucunDanger PLUS GRAND QUE Val_TauxCO PLUS PETIT QUE Ref_TauxCO_Danger| etape_MNT_SaisieCOAerer --> etape_MNT_SaisieCO
    etape_MNT_SaisieCO -->|Val_TauxCO PLUS GRAND OU EGALE A Ref_TauxCO_Danger| etape_MNT_SaisieCODanger --> etape_MNT_SaisieCO

    etape_MNT_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_GazMural| etape_MNT_CombustibleGaz --> etape_MNT_IdentificationChaudiere
    etape_MNT_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_GazAuSol| etape_MNT_CombustibleGaz
    etape_MNT_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_Fioul| etape_MNT_CombustibleFioul --> etape_MNT_IdentificationChaudiere
    etape_MNT_Choix_TypeChaudiere -->|Val_TypeChaudiere = _Ref_TypeChaudiere_Bois| etape_MNT_CombustibleBois --> etape_MNT_IdentificationChaudiere
    etape_MNT_Choix_TypeChaudiere --> etape_MNT_IdentificationChaudiere
    etape_MNT_IdentificationChaudiere --> etape_MNT_PhotoEnsemble

    etape_MNT_VerifChaudiere -->|Val_ModifierInfoChaudiere = Ref__BooleanTrue| etape_MNT_Choix_TypeChaudiere
    etape_MNT_VerifChaudiere --> etape_MNT_PhotoEnsemble --> etape_MNT_PhotoPlaqueSignaletique
    etape_MNT_PhotoPlaqueSignaletique --> etape_MNT_VerificationAvantIntervention --> etape_MNT_VerificationSecuriteChaudiere
    etape_MNT_VerificationSecuriteChaudiere -->|Val_VerificationSecuriteChaudiere = Ref_SecuriteChaudiereOK| etape_MNT_VerificationNettoyage
    etape_MNT_VerificationSecuriteChaudiere --> etape_MNT_SecuriteAquastat --> etape_MNT_SecuriteControleurDebit --> etape_MNT_SecuriteControleurPression
    etape_MNT_SecuriteControleurPression -->|Val_TypeFumisterie = Ref_Condensation| etape_MNT_SecuriteSondeIonisation
    etape_MNT_SecuriteControleurPression -->|Val_TypeFumisterie = Ref_Condensation_3CEP| etape_MNT_SecuriteSondeIonisation
    etape_MNT_SecuriteControleurPression -->|Val_TypeFumisterie = Ref_ConduitVMCGAZ| etape_MNT_SecuriteSPOTT
    etape_MNT_SecuriteControleurPression -->|Val_TypeFumisterie = Ref_ConduitFumee| etape_MNT_SecuriteSPOTT
    etape_MNT_SecuriteControleurPression -->|Val_TypeFumisterie = Ref_BasseTemperature| etape_MNT_SecuritePressostatDifferentiel
    etape_MNT_SecuriteControleurPression --> fin4
    etape_MNT_SecuriteSondeIonisation --> etape_MNT_SecuriteSoupape --> etape_MNT_VerificationNettoyage
    etape_MNT_SecuriteSPOTT --> etape_MNT_SecuriteThermocouple
    etape_MNT_SecuriteThermocouple -->|Val_TypeFumisterie = Ref_ConduitVMCGAZ| etape_MNT_SecuriteDSI
    etape_MNT_SecuriteThermocouple --> etape_MNT_SecuriteSondeIonisation
    etape_MNT_SecuriteDSI --> etape_MNT_SecuriteDSC --> etape_MNT_SecuriteSondeIonisation
    etape_MNT_SecuritePressostatDifferentiel --> etape_MNT_SecuriteSondeIonisation


    etape_MNT_VerificationNettoyage -->|Val_TypeFumisterie = Ref_Condensation| etape_MNT_Combustion2
    etape_MNT_VerificationNettoyage -->|Val_TypeFumisterie = Ref_Condensation_3CEP| etape_MNT_Combustion2
    etape_MNT_VerificationNettoyage --> etape_MNT_Combustion1
    etape_MNT_Combustion1 --> etape_MNT_VerificationApresIntervention
    etape_MNT_Combustion2 --> etape_MNT_Combustion3 --> etape_MNT_VerificationApresIntervention

    etape_MNT_VerificationApresIntervention --> etape_MNT_Saisie_CommentaireEntreprise

    etape_MNT_Saisie_CommentaireTech --> etape_MNT_Saisie_CommentaireEntreprise
    etape_MNT_Saisie_CommentaireEntreprise --> etape_MNT_Saisie_CommentaireClient
    etape_MNT_Saisie_CommentaireClient --> etape_MNT_SignatureClient
    etape_MNT_SignatureClient --> etape_MNT_Saisie_TempsIntervention
    etape_MNT_Saisie_TempsIntervention --> fin1
  end


```

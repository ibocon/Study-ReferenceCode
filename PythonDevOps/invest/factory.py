from lxml import etree
from pathlib import Path

from invest import model

class DocumentFactory(object):  
    """
        Factory Method 패턴을 반영하여, 제공된 filename에 따라 적합한 Document Instance가 생성된다.
    """
    @staticmethod
    def create_document(filename):
        # DocumentType을 결정하기 위해 파일 확장자를 확인한다.
        extension = Path(filename).suffix
        if(extension == ".xls"): # MS Office 기준
            return model._ExcelDocument(filename)
        elif(extension == ".xml" or extension == ".xsd"):
            return XbrlDocumentFactory.create_document(filename)


class XbrlDocumentFactory(DocumentFactory):
    """
        Root 태그에 따라 적합한 XBRL Document Instance가 생성된다.
    """
    @staticmethod
    def create_document(filename):

        tree = etree.parse(source = filename)
        element_tag = etree.QName(tree.getroot()).localname
        if(element_tag == "xbrl"):
            return model._InstanceDocument(filename, tree)
        elif(element_tag == "schema"):
            return model._SchemaDocument(filename, tree)
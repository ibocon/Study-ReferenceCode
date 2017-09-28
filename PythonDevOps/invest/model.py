"""
    문서의 내용을 Import하기 위해 Document Model을 정의한다.
"""
from lxml import etree
from abc import ABC, abstractmethod

class _AbstractDocument(ABC):
    """
        Document의 공통 인터페이스를 정의한다.
    """
    def __init__(self, filename):
        self._filename = filename

    @abstractmethod
    def get_document_type(self):
        return "Abstract"

class _ExcelDocument(_AbstractDocument):

    def __init__(self, filename):
        super().__init(self, filename)

    def get_document_type(self):
        return "Excel"


class _XbrlDocument(_AbstractDocument): 
    """
        XBRL 문서 인터페이스를 정의한다.
    """
    def __init__(self, filename, tree = None):
        super().__init__(filename)

        if(tree == None):
            self._tree = etree.parse(source = filename)
        else:
            assert isinstance(tree, etree._ElementTree)
            self._tree = tree

        self._root = self._tree.getroot()

    def get_namespace(self):
        assert isinstance(self._tree, etree._ElementTree)
        return self._tree.nsmap


class _SchemaDocument(_XbrlDocument):
    """
        XBRL 문서 중 Schema Document(.xsd)를 정의한다.
    """
    def __init__(self, filename, tree = None):
        super().__init__(filename, tree)

    def get_document_type(self):
        return "Schema"


class _InstanceDocument(_XbrlDocument):
    """
        XBRL 문서 중 Instance Document(.xml)를 정의한다.
    """
    def __init__(self, filename, tree):
        super().__init__(filename, tree)

    def get_document_type(self):
        return "Instance"
